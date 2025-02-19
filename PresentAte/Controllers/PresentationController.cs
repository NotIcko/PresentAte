using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentAte.Data;
using PresentAte.Data.Models;
using PresentAte.Services.Data.Interfaces;
using PresentAte.ViewModels.PresentationViewModels;

namespace PresentAte.Controllers
{
    public class PresentationController(
        IGoogleGeminiService geminiService,
        IPowerPointService pptService,
        PresentAteDbContext dbContext)
        : Controller
    {
        public async Task<IActionResult> Index()
        {
            var presentations = await dbContext.Presentations
                .Select(p => new PresentationViewModel
                {
                    Id = p.Id,
                    Topic = p.Topic,
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();

            return View(presentations);
        }

        public IActionResult Generate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GeneratePresentation(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
            {
                TempData["Error"] = "Please enter a topic!";
                return RedirectToAction("Generate");
            }

            try
            {
                string presentationContent = await geminiService.GeneratePresentationContent(topic);

                var slides = pptService.ParsePresentationContent(presentationContent);

                var powerpointFile = pptService.GeneratePresentationFile(topic, slides);

                var presentation = new Presentation
                {
                    Topic = topic,
                    FileContent = powerpointFile, 
                    CreatedAt = DateTime.UtcNow
                };

                dbContext.Presentations.Add(presentation);
                await dbContext.SaveChangesAsync();

                TempData["Success"] = $"Presentation on '{topic}' generated successfully!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred: {ex.Message}";
                return RedirectToAction("Generate");
            }
        }

        public async Task<IActionResult> DownloadPresentation(int id)
        {
            var presentation = await dbContext.Presentations.FindAsync(id);
            if (presentation == null)
            {
                return NotFound();
            }

            return File(presentation.FileContent, "application/vnd.openxmlformats-officedocument.presentationml.presentation", $"{presentation.Topic}.pptx");
        }
    }
}
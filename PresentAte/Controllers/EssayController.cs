using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PresentAte.Data.Models;
using PresentAte.Services.Data.Implementations;
using PresentAte.Services.Data.Interfaces;
using PresentAte.ViewModels.EssayViewModels;
using System.Security.Claims;

namespace PresentAte.Controllers
{
    public class EssayController(
        IEssayService essayService,
        UserManager<ApplicationUser> userManager)
        : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            var themes = essayService.GetAllThemes();
            var model = new EssayModel
            {
                AvailableThemes = themes.Select(t => new SelectListItem
                {
                    Value = t.ThemeId.ToString(),
                    Text = t.ThemeName
                }).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EssayModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    var userId = GetCurrentUserId();
            //    var essayId = await essayService.CreateEssay(userId, model.ThemeId, model.Content);
            //    return RedirectToAction("GetSuggestions", new { essayId = essayId });
            //}

            //model.AvailableThemes = essayService.GetAllThemes().Select(t => new SelectListItem
            //{
            //    Value = t.ThemeId.ToString(),
            //    Text = t.ThemeName
            //}).ToList();
            //return View(model);

            if (ModelState.IsValid)
            {
                var userId = GetCurrentUserId();
                var essayId = await essayService.CreateEssay(userId, model.ThemeId, model.Content);

                // Set success message
                TempData["SuccessMessage"] = "Your essay has been successfully submitted!";

                return RedirectToAction("Display"); // Redirect to a page where essays are displayed
            }

            // Repopulate themes and return the view if the model is invalid
            model.AvailableThemes = essayService.GetAllThemes()
                .Select(t => new SelectListItem
                {
                    Value = t.ThemeId.ToString(),
                    Text = t.ThemeName
                })
                .ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Display(int? themeId, string? userId)
        {
            // Fetch essays with comments
            var essays = essayService.GetEssaysWithComments();

            // Apply filters if provided
            //if (themeId.HasValue)
            //{
            //    essays = essays.Where(e => e.ThemeId == themeId.Value).ToList();
            //}

            //if (userId.HasValue)
            //{
            //    essays = essays.Where(e => e.UserId == userId.Value).ToList();
            //}

            // Populate ViewBag for filters
            ViewBag.Themes = essayService.GetAllThemes();
            ViewBag.Users = userManager.Users.ToList();

            return View(essays);
        }

        [HttpGet]
        public async Task<IActionResult> GetSuggestions(int essayId)
        {
            var suggestions = await essayService.GetEssaySuggestions(essayId);
            ViewBag.EssayId = essayId;
            return View("GetSuggestions", suggestions);
        }

        private string GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;
        }
    }
}

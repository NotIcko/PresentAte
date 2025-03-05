using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            var model = new EssayViewModel
            {
                AvailableThemes = themes.Select(t => t.ThemeName).ToList()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EssayViewModel model)
        {

            if (!ModelState.IsValid)
            {
                model.AvailableThemes = essayService.GetAllThemes().Select(t => t.ThemeName).ToList();

                return View(model);
            }

            try
            {
                var userId = GetCurrentUserId();
                await essayService.CreateEssay(model, userId);

                TempData["SuccessMessage"] = "Your essay has been successfully submitted!";

                return RedirectToAction("Display");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult Display(int? themeId, string? userId)
        {
            var essays = essayService.GetEssaysWithComments(themeId);
            ViewBag.Themes = essayService.GetAllThemes();
            ViewBag.Users = userManager.Users.ToList();

            ViewBag.SelectedThemeId = themeId;
            ViewBag.SelectedUserId = userId;

            return View(essays);
        }

        [HttpGet]
        public async Task<IActionResult> EditEssay(int essayId)
        {
            var essay = await essayService.GetEssayByIdAsync(essayId);
            if (essay == null)
            {
                TempData["ErrorMessage"] = "Essay not found.";
                return RedirectToAction("Display");
            }

            ViewBag.Themes = essayService.GetAllThemes();
            return View(essay);
        }

        [HttpPost]
        public async Task<IActionResult> EditEssay(int essayId, string content, int themeId)
        {
            try
            {
                await essayService.UpdateEssayAsync(essayId, content, themeId);
                TempData["SuccessMessage"] = "Essay updated successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Display");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEssay(int essayId)
        {
            try
            {
                await essayService.DeleteEssayAsync(essayId);
                TempData["SuccessMessage"] = "Essay deleted successfully!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction("Display");
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(int essayId, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                TempData["ErrorMessage"] = "Comment cannot be empty.";
                return RedirectToAction("Display");
            }

            string userId = GetCurrentUserId();

            await essayService.CreateComment(essayId, content, userId);

            TempData["SuccessMessage"] = "Comment added successfully!";
            return RedirectToAction("Display");
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

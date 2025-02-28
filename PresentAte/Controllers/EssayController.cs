using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PresentAte.Data.Models;
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
            if (ModelState.IsValid)
            {
                var userId = GetCurrentUserId();
                var essayId = await essayService.CreateEssay(userId, model.ThemeId, model.Content);
                return RedirectToAction("GetSuggestions", new { essayId = essayId });
            }

            model.AvailableThemes = essayService.GetAllThemes().Select(t => new SelectListItem
            {
                Value = t.ThemeId.ToString(),
                Text = t.ThemeName
            }).ToList();
            return View(model);
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

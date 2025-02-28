using Microsoft.AspNetCore.Mvc.Rendering;

namespace PresentAte.ViewModels.EssayViewModels
{
    public class EssayModel
    {
        public int ThemeId { get; set; }
        public string Content { get; set; }

        public List<SelectListItem> AvailableThemes { get; set; }
    }
}

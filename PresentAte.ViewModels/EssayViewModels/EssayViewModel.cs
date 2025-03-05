namespace PresentAte.ViewModels.EssayViewModels
{
    public class EssayViewModel
    {
        public string ThemeName { get; set; }

        public string Content { get; set; } = null!;

        public List<string> AvailableThemes { get; set; } = new List<string>();
    }
}

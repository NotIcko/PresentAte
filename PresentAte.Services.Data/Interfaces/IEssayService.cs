using PresentAte.Data.Models;

namespace PresentAte.Services.Data.Interfaces
{
    public interface IEssayService
    {
        Task<string> CreateEssay(string userId, int themeId, string content);
        List<EssayTheme> GetAllThemes();
        Task<string> GetEssaySuggestions(int essayId);
        List<DisplayEssayViewModel> GetEssaysWithComments();
    }
}

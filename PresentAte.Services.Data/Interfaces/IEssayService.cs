using PresentAte.Data.Models;
using PresentAte.ViewModels.EssayViewModels;

namespace PresentAte.Services.Data.Interfaces
{
    public interface IEssayService
    {
        Task CreateEssay(EssayViewModel model, string userId);
        List<EssayTheme> GetAllThemes();
        Task<string> GetEssaySuggestions(int essayId);
        List<DisplayEssayViewModel> GetEssaysWithComments(int? themeId);
        Task CreateComment(int essayId, string content, string userId);
        Task<Essay> GetEssayByIdAsync(int essayId);
        Task UpdateEssayAsync(int essayId, string content, int themeId);
        Task DeleteEssayAsync(int essayId);
    }
}

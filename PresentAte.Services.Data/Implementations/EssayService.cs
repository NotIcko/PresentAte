using Microsoft.EntityFrameworkCore;
using PresentAte.Data;
using PresentAte.Data.Models;
using PresentAte.Services.Data.Interfaces;
using PresentAte.ViewModels.EssayViewModels;

namespace PresentAte.Services.Data.Implementations
{
    public class EssayService(
        PresentAteDbContext dbContext)
        : IEssayService
    {
        public async Task CreateEssay(EssayViewModel model, string userId)
        {
            var theme = await dbContext.EssayThemes.FirstOrDefaultAsync(t => t.ThemeName == model.ThemeName);
            if (theme == null)
            {
                throw new Exception("Selected theme does not exist.");
            }

            var essay = new Essay
            {
                UserId = userId,
                ThemeId = theme.ThemeId,
                Content = model.Content,
                CreatedAt = DateTime.UtcNow,
                IsPublished = true
            };

            dbContext.Essays.Add(essay);
            await dbContext.SaveChangesAsync();
        }

        public List<EssayTheme> GetAllThemes()
        {
            return dbContext.EssayThemes.ToList();
        }

        public Task<string> GetEssaySuggestions(int essayId)
        {
            throw new NotImplementedException();
        }

        public List<DisplayEssayViewModel> GetEssaysWithComments(int? themeId)
        {
            var query = dbContext.Essays
                .Include(e => e.User)
                .Include(e => e.Theme)
                .Include(e => e.Comments)
                .ThenInclude(c => c.User)
                .AsQueryable();

            if (themeId.HasValue)
            {
                query = query.Where(e => e.Theme.ThemeId == themeId.Value);
            }

            return query.Select(e => new DisplayEssayViewModel
            {
                EssayId = e.EssayId,
                Content = e.Content,
                UserName = e.User.UserName,
                ThemeName = e.Theme.ThemeName,
                CreatedAt = e.CreatedAt,
                Comments = e.Comments.Select(c => new CommentViewModel
                {
                    Content = c.Content,
                    UserName = c.User.UserName,
                    CreatedAt = c.CreatedAt
                }).ToList()
            })
            .ToList();
        }

        public async Task CreateComment(int essayId, string content, string userId)
        {
            var comment = new Comment
            {
                EssayId = essayId,
                Content = content,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            dbContext.Comments.Add(comment);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Essay> GetEssayByIdAsync(int essayId)
        {
            return await dbContext.Essays
                .Include(e => e.Theme)
                .FirstOrDefaultAsync(e => e.EssayId == essayId);
        }

        public async Task UpdateEssayAsync(int essayId, string content, int themeId)
        {
            var essay = await dbContext.Essays.FindAsync(essayId);
            if (essay == null)
            {
                throw new ArgumentException("Essay not found.");
            }

            essay.Content = content;
            essay.ThemeId = themeId;
            await dbContext.SaveChangesAsync();
        }

        public async Task DeleteEssayAsync(int essayId)
        {
            var essay = await dbContext.Essays.FindAsync(essayId);
            if (essay == null)
            {
                throw new ArgumentException("Essay not found.");
            }

            dbContext.Essays.Remove(essay);
            await dbContext.SaveChangesAsync();
        }
        //public async Task<string> GetEssaySuggestions(int essayId)
        //{
        //    var essay = await dbContext.Essays.FindAsync(essayId);
        //    if (essay == null)
        //    {
        //        throw new ArgumentException("Essay not found");
        //    }

        //    var suggestions = await CallAIForSuggestions(essay.Content);
        //    return suggestions;
        //}

        //private async Task<string> CallAIForSuggestions(string content)
        //{
        //    // Example using OpenAI API
        //    var openAiClient = new OpenAIClient("your-api-key");
        //    var prompt = $"Analyze this Bulgarian essay and provide suggestions for improvement:\n\n{content}";
        //    var response = await openAiClient.Completions.CreateCompletionAsync(prompt, maxTokens: 500);
        //    return response.Choices[0].Text;
        //}
    }
}

using Microsoft.AspNetCore.Identity;
using PresentAte.Data;
using PresentAte.Data.Models;
using PresentAte.Services.Data.Interfaces;

namespace PresentAte.Services.Data.Implementations
{
    public class EssayService(
        PresentAteDbContext dbContext)
        : IEssayService
    {
        public async Task<string> CreateEssay(string userId, int themeId, string content)
        {
            var essay = new Essay
            {
                UserId = userId,
                ThemeId = themeId,
                Content = content,
                CreatedAt = DateTime.UtcNow,
                IsPublished = false
            };

            dbContext.Essays.Add(essay);
            await dbContext.SaveChangesAsync();
            return essay.EssayId;
        }

        public List<EssayTheme> GetAllThemes()
        {
            return dbContext.EssayThemes.ToList();
        }

        public Task<string> GetEssaySuggestions(int essayId)
        {
            throw new NotImplementedException();
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

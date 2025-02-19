using Microsoft.Extensions.Configuration;
using PresentAte.Services.Data.Interfaces;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using static PresentAte.Common.ApplicationConstants.GeminiAIEndpoint;

namespace PresentAte.Services.Data.Implementations
{
    public class GoogleGeminiService(
        HttpClient httpClient,
        IConfiguration configuration)
        : IGoogleGeminiService
    {
        private readonly string? apiKey = configuration["GoogleAI:ApiKey"];

        public async Task<string> GeneratePresentationContent(string topic)
        {
            string endpoint = AIEndpoint + apiKey;

            var requestData = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = $"Create an outline for a PowerPoint presentation on {topic}. Follow these formatting rules strictly: " +
                                 "1. Every slide title must start with '**Slide Title:' and end with '**'. " +
                                 "2. Each bullet point must start with '* '. " +
                                 "3. Do not include any additional formatting or explanations. " +
                                 "4. Ensure all content is structured as slide titles and bullet points." }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}

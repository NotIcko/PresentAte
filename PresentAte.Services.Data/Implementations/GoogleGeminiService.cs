using Microsoft.Extensions.Configuration;
using PresentAte.Services.Data.Interfaces;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace PresentAte.Services.Data.Implementations
{
    public class GoogleGeminiService(
        HttpClient httpClient,
        IConfiguration configuration)
        : IGoogleGeminiService
    {
        private readonly string? apiKey = configuration["GoogleAI:ApiKey"];
        private readonly string aiEndpoint = "https://generativelanguage.googleapis.com/v1/models/gemini-2.0:generateContent";


        public async Task<string> GeneratePresentationContent(string topic)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("API key not configured properly.");
            }

            string endpoint = aiEndpoint + apiKey;

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

            try
            {
                var response = await httpClient.PostAsync(endpoint, content);

                if (!response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    return $"Error: {response.StatusCode}";
                }

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}

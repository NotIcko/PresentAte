using DocumentFormat.OpenXml.Packaging;

namespace PresentAte.Services.Data.Interfaces
{
    public interface IGoogleGeminiService
    {
        Task<string> GeneratePresentationContent(string topic);
    }
}

using DocumentFormat.OpenXml.Packaging;

namespace PresentAte.Services.Data.Interfaces
{
    public interface IPowerPointService
    {
        List<(string Title, string Content)> ParsePresentationContent(string content);
        byte[] GeneratePresentationFile(string topic, List<(string Title, string Content)> slides);
    }
}

using System.Text;
using Newtonsoft.Json.Linq;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using PresentationDrawing = DocumentFormat.OpenXml.Drawing;
using Presentation = DocumentFormat.OpenXml.Presentation;
using PresentAte.Services.Data.Interfaces;
using PresentAte.Data;
using Microsoft.Extensions.Logging;

namespace PresentAte.Services.Data.Implementations
{
    public class PowerPointService(
        PresentAteDbContext dbContext)
        : IPowerPointService
    {
        public List<(string Title, string Content)> ParsePresentationContent(string content)
        {
            var slides = new List<(string Title, string Content)>();

            var jsonObject = JObject.Parse(content);
            var text = jsonObject["candidates"]?[0]?["content"]?["parts"]?[0]?["text"]?.ToString();

            if (text == null)
            {
                throw new NullReferenceException("Invlid json");
            }

            var lines = text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string currentTitle = "";
            var currentContent = new StringBuilder();

            foreach (var line in lines)
            {
                if (line.StartsWith("**Slide Title:"))
                {
                    if (!string.IsNullOrEmpty(currentTitle))
                    {
                        slides.Add((currentTitle, currentContent.ToString().Trim()));
                        currentContent.Clear();
                    }
                    currentTitle = line.Replace("**Slide Title:", "").Trim('*', ' ', ':');
                }
                else if (line.StartsWith("* "))
                {
                    currentContent.AppendLine(line.Trim());
                }
            }

            if (!string.IsNullOrEmpty(currentTitle))
            {
                slides.Add((currentTitle, currentContent.ToString().Trim()));
            }

            return slides;
        }
        //public byte[] GeneratePresentationFile(string topic, List<(string Title, string Content)> slides)
        //{
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        using (PresentationDocument ppt = PresentationDocument.Create(memoryStream, PresentationDocumentType.Presentation))
        //        {
        //            PresentationPart presentationPart = ppt.AddPresentationPart();
        //            presentationPart.Presentation = new Presentation.Presentation();

        //            SlideMasterPart slideMasterPart = presentationPart.AddNewPart<SlideMasterPart>();
        //            slideMasterPart.SlideMaster = new SlideMaster(
        //                new CommonSlideData(
        //                    new ShapeTree(
        //                        new Presentation.Shape(
        //                            new Presentation.TextBody(
        //                                new PresentationDrawing.BodyProperties(),
        //                                new PresentationDrawing.ListStyle(),
        //                                new PresentationDrawing.Paragraph(new PresentationDrawing.Run(new PresentationDrawing.Text("Slide Master")))
        //                        )
        //                    )
        //                ),
        //                new Presentation.ColorMap() { Background1 = PresentationDrawing.ColorSchemeIndexValues.Light1, Text1 = PresentationDrawing.ColorSchemeIndexValues.Dark1 }
        //            ));
        //            slideMasterPart.SlideMaster.Save();

        //            SlideLayoutPart slideLayoutPart = slideMasterPart.AddNewPart<SlideLayoutPart>();
        //            slideLayoutPart.SlideLayout = new SlideLayout(
        //                new CommonSlideData(
        //                    new ShapeTree(
        //                        new Presentation.Shape(
        //                            new Presentation.TextBody(
        //                                new PresentationDrawing.BodyProperties(),
        //                                new PresentationDrawing.ListStyle(),
        //                                new PresentationDrawing.Paragraph(new PresentationDrawing.Run(new PresentationDrawing.Text("Slide Layout")))
        //                            )
        //                        )
        //                    )
        //                ),
        //                new Presentation.ColorMap() { Background1 = PresentationDrawing.ColorSchemeIndexValues.Light1, Text1 = PresentationDrawing.ColorSchemeIndexValues.Dark1 }
        //            );
        //            slideLayoutPart.SlideLayout.Save();

        //            SlidePart titleSlidePart = presentationPart.AddNewPart<SlidePart>();
        //            titleSlidePart.Slide = new Slide(
        //                new CommonSlideData(
        //                    new ShapeTree(
        //                        new Presentation.Shape(
        //                            new Presentation.TextBody(
        //                                new PresentationDrawing.BodyProperties(),
        //                                new PresentationDrawing.ListStyle(),
        //                                new PresentationDrawing.Paragraph(new PresentationDrawing.Run(new PresentationDrawing.Text(topic)))
        //                        )
        //                    )
        //                )
        //            ));
        //            titleSlidePart.Slide.Save();

        //            var slideIdList = new SlideIdList();
        //            uint slideId = 256;

        //            foreach (var slide in slides)
        //            {
        //                SlidePart slidePart = presentationPart.AddNewPart<SlidePart>();
        //                slidePart.Slide = new Slide(
        //                    new CommonSlideData(
        //                        new ShapeTree(
        //                            new Presentation.Shape(
        //                                new Presentation.TextBody(
        //                                    new PresentationDrawing.BodyProperties(),
        //                                    new PresentationDrawing.ListStyle(),
        //                                    new PresentationDrawing.Paragraph(new PresentationDrawing.Run(new PresentationDrawing.Text(slide.Title)))
        //                            ),
        //                            new Presentation.Shape(
        //                                new Presentation.TextBody(
        //                                    new PresentationDrawing.BodyProperties(),
        //                                    new PresentationDrawing.ListStyle(),
        //                                    new PresentationDrawing.Paragraph(new PresentationDrawing.Run(new PresentationDrawing.Text(slide.Content)))
        //                            )
        //                        )
        //                    )
        //                )));
        //                slidePart.Slide.Save();

        //                slideIdList.AppendChild(new SlideId { Id = slideId++, RelationshipId = presentationPart.GetIdOfPart(slidePart) });
        //            }

        //            presentationPart.Presentation.AppendChild(slideIdList);
        //            presentationPart.Presentation.Save();
        //        }

        //        return memoryStream.ToArray();
        //    }
        //}

        public byte[] GeneratePresentationFile(string topic, List<(string Title, string Content)> slides)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (PresentationDocument ppt = PresentationDocument.Create(memoryStream, PresentationDocumentType.Presentation))
                {
                    PresentationPart presentationPart = ppt.AddPresentationPart();
                    presentationPart.Presentation = new Presentation.Presentation();

                    SlideMasterPart slideMasterPart = presentationPart.AddNewPart<SlideMasterPart>();
                    slideMasterPart.SlideMaster = new SlideMaster(
                        new CommonSlideData(
                            new ShapeTree(
                                new Presentation.Shape(
                                    new Presentation.TextBody(
                                        new PresentationDrawing.BodyProperties(),
                                        new PresentationDrawing.ListStyle(),
                                        new PresentationDrawing.Paragraph(new PresentationDrawing.Run(new PresentationDrawing.Text("Slide Master")))
                                    )
                                )
                            )
                        ),
                        new Presentation.ColorMap() { Background1 = PresentationDrawing.ColorSchemeIndexValues.Light1, Text1 = PresentationDrawing.ColorSchemeIndexValues.Dark1 }
                    );
                    slideMasterPart.SlideMaster.Save();

                    SlideLayoutPart slideLayoutPart = slideMasterPart.AddNewPart<SlideLayoutPart>();
                    slideLayoutPart.SlideLayout = new SlideLayout(
                        new CommonSlideData(
                            new ShapeTree(
                                new Presentation.Shape(
                                    new Presentation.TextBody(
                                        new PresentationDrawing.BodyProperties(),
                                        new PresentationDrawing.ListStyle(),
                                        new PresentationDrawing.Paragraph(new PresentationDrawing.Run(new PresentationDrawing.Text("Slide Layout")))
                                    )
                                )
                            )
                        ),
                        new Presentation.ColorMap() { Background1 = PresentationDrawing.ColorSchemeIndexValues.Light1, Text1 = PresentationDrawing.ColorSchemeIndexValues.Dark1 }
                    );
                    slideLayoutPart.SlideLayout.Save();

                    SlidePart titleSlidePart = presentationPart.AddNewPart<SlidePart>();
                    titleSlidePart.Slide = new Slide(
                        new CommonSlideData(
                            new ShapeTree(
                                new Presentation.Shape(
                                    new Presentation.TextBody(
                                        new PresentationDrawing.BodyProperties(),
                                        new PresentationDrawing.ListStyle(),
                                        new PresentationDrawing.Paragraph(new PresentationDrawing.Run(new PresentationDrawing.Text(topic))))
                                )
                            )
                        )
                    );
                    titleSlidePart.Slide.Save();

                    var slideIdList = new SlideIdList();

                    uint slideId = 256;
                    foreach (var slide in slides)
                    {
                        SlidePart slidePart = presentationPart.AddNewPart<SlidePart>();
                        slidePart.Slide = new Slide(
                            new CommonSlideData(
                                new ShapeTree(
                                    new Presentation.Shape(
                                        new Presentation.TextBody(
                                            new PresentationDrawing.BodyProperties(),
                                            new PresentationDrawing.ListStyle(),
                                            new PresentationDrawing.Paragraph(new PresentationDrawing.Run(new PresentationDrawing.Text(slide.Title))))
                                    ),
                                    new Presentation.Shape(
                                        new Presentation.TextBody(
                                            new PresentationDrawing.BodyProperties(),
                                            new PresentationDrawing.ListStyle(),
                                            new PresentationDrawing.Paragraph(new PresentationDrawing.Run(new PresentationDrawing.Text(slide.Content))))
                                    )
                                )
                            )
                        );
                        slidePart.Slide.Save();

                        slideIdList.AppendChild(new SlideId { Id = slideId++, RelationshipId = presentationPart.GetIdOfPart(slidePart) });
                    }

                    presentationPart.Presentation.AppendChild(slideIdList);
                    presentationPart.Presentation.Save();
                }

                return memoryStream.ToArray();
            }
        }

        private void AddTitleToSlide(SlidePart slidePart, string titleText)
        {
            var shapeTree = slidePart.Slide.Elements<CommonSlideData>().First().Elements<ShapeTree>().First();
            var titleShape = new Shape();
            var textBody = new TextBody(new PresentationDrawing.Paragraph(new PresentationDrawing.Run(new Text(titleText))));
            titleShape.AppendChild(textBody);
            shapeTree.AppendChild(titleShape);
        }

        private void AddBulletPointsToSlide(SlidePart slidePart, string content)
        {
            var shapeTree = slidePart.Slide.Elements<CommonSlideData>().First().Elements<ShapeTree>().First();
            var contentShape = new Shape();
            var textBody = new TextBody();

            foreach (var bulletPoint in content.Split('\n'))
            {
                textBody.AppendChild(new PresentationDrawing.Paragraph(new PresentationDrawing.Run(new Text(bulletPoint))));
            }

            contentShape.AppendChild(textBody);
            shapeTree.AppendChild(contentShape);
        }

        public bool DeletePresentation(int presentationId)
        {
            var presentation = dbContext.Presentations.Find(presentationId);
            if (presentation == null)
            {
                throw new KeyNotFoundException($"Presentation with ID {presentationId} not found.");
            }

            dbContext.Presentations.Remove(presentation);
            dbContext.SaveChanges();

            return true;
        }
    }
}

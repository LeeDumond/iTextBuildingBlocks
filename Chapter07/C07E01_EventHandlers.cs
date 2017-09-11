using System.IO;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter07
{
    public class C07E01_EventHandlers
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter07\jekyll_hyde_page_orientation.pdf";

        private static readonly PdfNumber INVERTEDPORTRAIT = new PdfNumber(180);
        private static readonly PdfNumber LANDSCAPE = new PdfNumber(90);
        private static readonly PdfNumber PORTRAIT = new PdfNumber(0);
        private static readonly PdfNumber SEASCAPE = new PdfNumber(270);

        public static void Main(string[] args)
        {
            FileInfo file = new FileInfo(DEST);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            CreatePdf(DEST);
        }

        private static void CreatePdf(string dest)
        {
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            pdf.GetCatalog().SetPageLayout(PdfName.TwoColumnLeft);
            PageRotationEventHandler eventHandler = new PageRotationEventHandler();
            pdf.AddEventHandler(PdfDocumentEvent.START_PAGE, eventHandler);

            Document document = new Document(pdf, PageSize.A8);

            document.Add(new Paragraph("Dr. Jekyll"));

            eventHandler.SetRotation(INVERTEDPORTRAIT);
            document.Add(new AreaBreak());
            document.Add(new Paragraph("Mr. Hyde"));

            eventHandler.SetRotation(LANDSCAPE);
            document.Add(new AreaBreak());
            document.Add(new Paragraph("Dr. Jekyll"));

            eventHandler.SetRotation(SEASCAPE);
            document.Add(new AreaBreak());
            document.Add(new Paragraph("Mr. Hyde"));

            document.Close();
        }

        private class PageRotationEventHandler : IEventHandler
        {
            private PdfNumber rotation = PORTRAIT;

            public void SetRotation(PdfNumber orientation)
            {
                rotation = orientation;
            }

            public void HandleEvent(Event @event)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent) @event;
                docEvent.GetPage().Put(PdfName.Rotate, rotation);
            }
        }
    }
}
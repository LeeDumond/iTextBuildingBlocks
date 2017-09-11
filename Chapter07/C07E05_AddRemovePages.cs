using System;
using System.IO;
using iText.Kernel.Events;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter07
{
    public class C07E05_AddRemovePages
    {
        private static readonly string SRC = $@"{Paths.PdfResourcesPath}\jekyll_hyde_bookmarked.pdf";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter07\jekyll_hyde_updated.pdf";

        public static void Main(string[] args)
        {
            FileInfo file = new FileInfo(DEST);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            ManipulatePdf(SRC, DEST);
        }

        private static void ManipulatePdf(string src, string dest)
        {
            PdfReader reader = new PdfReader(src);
            PdfWriter writer = new PdfWriter(dest);
            PdfDocument pdf = new PdfDocument(reader, writer);
            pdf.AddEventHandler(PdfDocumentEvent.INSERT_PAGE, new AddPageHandler());
            pdf.AddEventHandler(PdfDocumentEvent.REMOVE_PAGE, new RemovePageHandler());
            pdf.AddNewPage(1, PageSize.A4);
            int total = pdf.GetNumberOfPages();

            for (int i = 9; i <= total; i++)
            {
                pdf.RemovePage(9);
                if (i == 12)
                {
                    pdf.RemoveAllHandlers();
                }
            }

            pdf.Close();
        }

        private class AddPageHandler : IEventHandler
        {
            public void HandleEvent(Event @event)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent) @event;
                PdfDocument pdf = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();
                PdfCanvas pdfCanvas = new PdfCanvas(page);
                Canvas canvas = new Canvas(pdfCanvas, pdf, page.GetPageSize());
                canvas.Add(new Paragraph().Add(docEvent.GetEventType()));
            }
        }

        private class RemovePageHandler : IEventHandler
        {
            public void HandleEvent(Event @event)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent) @event;
                Console.WriteLine(docEvent.GetEventType());
            }
        }
    }
}
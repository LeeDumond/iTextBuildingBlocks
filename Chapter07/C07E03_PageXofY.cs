using System.IO;
using iText.IO.Font;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Hyphenation;
using iText.Layout.Properties;
using iTextBuildingBlocks;

namespace Chapter07
{
    public class C07E03_PageXofY
    {
        private static readonly string SRC = $@"{Paths.TextResourcesPath}\jekyll_hyde.txt";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter07\jekyll_hydeV2.pdf";

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
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));

            pdf.AddEventHandler(PdfDocumentEvent.START_PAGE,
                new Header("The Strange Case of Dr. Jekyll and Mr. Hyde"));

            PageXofY @event = new PageXofY(pdf);
            pdf.AddEventHandler(PdfDocumentEvent.END_PAGE, @event);

            // Initialize document
            Document document = new Document(pdf);
            PdfFont font = PdfFontFactory.CreateFont(FontConstants.TIMES_ROMAN);
            PdfFont bold = PdfFontFactory.CreateFont(FontConstants.HELVETICA_BOLD);
            document.SetTextAlignment(TextAlignment.JUSTIFIED)
                .SetHyphenation(new HyphenationConfig("en", "uk", 3, 3));

            StreamReader sr = File.OpenText(SRC);
            string line;
            Div div = new Div();
            while ((line = sr.ReadLine()) != null)
            {
                document.Add(new Paragraph(line)
                    .SetFont(bold).SetFontSize(12)
                    .SetMarginBottom(0)
                    .SetKeepWithNext(true));
                div = new Div()
                    .SetFont(font).SetFontSize(11)
                    .SetMarginBottom(18);
                while ((line = sr.ReadLine()) != null)
                {
                    div.Add(
                        new Paragraph(line)
                            .SetMarginBottom(0)
                            .SetFirstLineIndent(36)
                            .SetMultipliedLeading(1.2f)
                    );
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        document.Add(div);
                        break;
                    }
                }
            }
            document.Add(div);

            @event.WriteTotal(pdf);

            //Close document
            document.Close();
        }

        private class Header : IEventHandler
        {
            private readonly string header;

            public Header(string header)
            {
                this.header = header;
            }

            public void HandleEvent(Event @event)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent) @event;
                PdfDocument pdf = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();
                if (pdf.GetPageNumber(page) == 1)
                {
                    return;
                }

                Rectangle pageSize = page.GetPageSize();

                PdfCanvas pdfCanvas = new PdfCanvas(
                    page.GetLastContentStream(), page.GetResources(), pdf);

                Canvas canvas = new Canvas(pdfCanvas, pdf, pageSize);
                canvas.ShowTextAligned(header,
                    pageSize.GetWidth() / 2,
                    pageSize.GetTop() - 30, TextAlignment.CENTER);
            }
        }

        private class PageXofY : IEventHandler
        {
            private readonly PdfFormXObject placeholder;
            private const float side = 20;
            private const float x = 300;
            private const float y = 25;
            private const float space = 4.5f;
            private const float descent = 3;

            public PageXofY(PdfDocument pdf)
            {
                placeholder = new PdfFormXObject(new Rectangle(0, 0, side, side));
            }

            public void HandleEvent(Event @event)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent) @event;
                PdfDocument pdf = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();
                int pageNumber = pdf.GetPageNumber(page);
                Rectangle pageSize = page.GetPageSize();
                PdfCanvas pdfCanvas = new PdfCanvas(
                    page.GetLastContentStream(), page.GetResources(), pdf);
                Canvas canvas = new Canvas(pdfCanvas, pdf, pageSize);
                Paragraph p = new Paragraph()
                    .Add("Page ").Add(pageNumber.ToString()).Add(" of");
                canvas.ShowTextAligned(p, x, y, TextAlignment.RIGHT);
                pdfCanvas.AddXObject(placeholder, x + space, y - descent);
                pdfCanvas.Release();
            }

            public void WriteTotal(PdfDocument pdf)
            {
                Canvas canvas = new Canvas(placeholder, pdf);
                canvas.ShowTextAligned(pdf.GetNumberOfPages().ToString(), 0, descent, TextAlignment.LEFT);
            }
        }
    }
}
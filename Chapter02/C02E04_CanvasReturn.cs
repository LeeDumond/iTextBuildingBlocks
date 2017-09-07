using System.IO;
using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter02
{
    public class C02E04_CanvasReturn
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter02\canvas_return.pdf";

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

            PdfPage page = pdf.AddNewPage();
            PdfCanvas pdfCanvas = new PdfCanvas(page);

            Rectangle rectangle = new Rectangle(36, 650, 100, 100);
            pdfCanvas.Rectangle(rectangle);
            pdfCanvas.Stroke();

            Canvas canvas1 = new Canvas(pdfCanvas, pdf, rectangle);

            PdfFont font = PdfFontFactory.CreateFont(FontConstants.TIMES_ROMAN);
            PdfFont bold = PdfFontFactory.CreateFont(FontConstants.TIMES_BOLD);

            Text title = new Text("The Strange Case of Dr. Jekyll and Mr. Hyde").SetFont(bold);
            Text author = new Text("Robert Louis Stevenson").SetFont(font);
            Paragraph p = new Paragraph().Add(title).Add(" by ").Add(author);
            canvas1.Add(p);

            PdfPage page2 = pdf.AddNewPage();
            PdfCanvas pdfCanvas2 = new PdfCanvas(page2);

            Canvas canvas2 = new Canvas(pdfCanvas2, pdf, rectangle);
            canvas2.Add(new Paragraph("Dr. Jekyll and Mr. Hyde"));

            PdfPage page1 = pdf.GetFirstPage();
            PdfCanvas pdfCanvas1 = new PdfCanvas(
                page1.NewContentStreamBefore(), page1.GetResources(), pdf);

            rectangle = new Rectangle(100, 700, 100, 100);

            pdfCanvas1.
                SaveState()
                .SetFillColor(Color.CYAN)
                .Rectangle(rectangle)
                .Fill()
                .RestoreState();

            Canvas canvas = new Canvas(pdfCanvas1, pdf, rectangle);

            canvas.Add(new Paragraph("Dr. Jekyll and Mr. Hyde"));

            //Close document
            pdf.Close();
        }
    }
}
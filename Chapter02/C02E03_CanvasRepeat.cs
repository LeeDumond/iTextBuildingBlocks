using System.IO;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using iTextBuildingBlocks;

namespace Chapter02
{
    public class C02E03_CanvasRepeat
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter02\canvas_repeat.pdf";

        public static void Main(string[] args)
        {
            FileInfo file = new FileInfo(DEST);
            if (file.Directory != null && !file.Directory.Exists)
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
            Rectangle rectangle = new Rectangle(36, 500, 100, 250);
            pdfCanvas.Rectangle(rectangle);
            pdfCanvas.Stroke();
            Canvas canvas = new Canvas(pdfCanvas, pdf, rectangle);
            MyCanvasRenderer renderer = new MyCanvasRenderer(canvas);
            canvas.SetRenderer(renderer);
            PdfFont font = PdfFontFactory.CreateFont(FontConstants.TIMES_ROMAN);
            PdfFont bold = PdfFontFactory.CreateFont(FontConstants.TIMES_BOLD);
            Text title = new Text("The Strange Case of Dr. Jekyll and Mr. Hyde").SetFont(bold);
            Text author = new Text("Robert Louis Stevenson").SetFont(font);
            Paragraph p = new Paragraph().Add(title).Add(" by ").Add(author);

            while (!renderer.IsFull())
            {
                canvas.Add(p);
            }
                
            //Close document
            pdf.Close();
        }

        private class MyCanvasRenderer : CanvasRenderer
        {
            private bool _full;

            public MyCanvasRenderer(Canvas canvas) : base(canvas)
            {
            }

            public override void AddChild(IRenderer renderer)
            {
                base.AddChild(renderer);
                _full = this.GetPropertyAsBoolean(Property.FULL).Equals(true);
            }

            public bool IsFull()
            {
                return _full;
            }
        }
    }
}
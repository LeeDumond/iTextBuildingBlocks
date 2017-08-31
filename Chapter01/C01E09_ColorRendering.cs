using System.IO;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;

namespace Chapter01
{
    public class C01E09_ColorRendering
    {
private const string DEST = @"C:\Projects2\iTextBuildingBlocks\results\chapter01\color_rendermode.pdf";

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
            // Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));

            // Initialize document
            Document document = new Document(pdf);

            // Add content
            Text title1 = new Text("The Strange Case of ").SetFontColor(Color.BLUE);

            Text title2 = new Text("Dr. Jekyll")
                .SetStrokeColor(Color.GREEN)
                .SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.FILL_STROKE);

            Text title3 = new Text(" and ");

            Text title4 = new Text("Mr. Hyde")
                .SetStrokeColor(Color.RED)
                .SetStrokeWidth(0.5f)
                .SetTextRenderingMode(PdfCanvasConstants.TextRenderingMode.STROKE);

            Paragraph p = new Paragraph().SetFontSize(24)
                .Add(title1).Add(title2).Add(title3).Add(title4);

            document.Add(p);

            //Close document
            document.Close();
        }
    }
}
using System.IO;
using iText.IO.Font;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace Chapter01
{
    public class C01E10_ReusingStyles
    {
        private const string DEST = @"C:\Projects2\iTextBuildingBlocks\results\chapter01\style_example.pdf";

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
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));

            Document document = new Document(pdf);

            Style normal = new Style();
            PdfFont font = PdfFontFactory.CreateFont(FontConstants.TIMES_ROMAN);
            normal.SetFont(font).SetFontSize(14);

            Style code = new Style();
            PdfFont monospace = PdfFontFactory.CreateFont(FontConstants.COURIER);
            code.SetFont(monospace).SetFontColor(Color.RED).SetBackgroundColor(Color.LIGHT_GRAY);

            Paragraph p = new Paragraph();

            p.Add(new Text("The Strange Case of ").AddStyle(normal));
            p.Add(new Text("Dr. Jekyll").AddStyle(code));
            p.Add(new Text(" and ").AddStyle(normal));
            p.Add(new Text("Mr. Hyde").AddStyle(code));
            p.Add(new Text(".").AddStyle(normal));

            document.Add(p);

            document.Close();
        }
    }
}
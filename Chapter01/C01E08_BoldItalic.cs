using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace Chapter01
{
    public class C01E08_BoldItalic
    {
        private const string DEST = @"C:\Projects2\iTextBuildingBlocks\results\chapter01\bold_italic.pdf";

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
            Text title1 = new Text("The Strange Case of ").SetItalic();
            Text title2 = new Text("Dr. Jekyll and Mr. Hyde").SetBold();
            Text author = new Text("Robert Louis Stevenson").SetItalic().SetBold();

            Paragraph p = new Paragraph()
                .Add(title1)
                .Add(title2)
                .Add(" by ")
                .Add(author);

            document.Add(p);

            //Close document
            document.Close();
        }
    }
}
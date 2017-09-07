using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter01
{
    public class C01E07_FontSize
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter01\font_size.pdf";

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
            // Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));

            // Initialize document
            Document document = new Document(pdf);

            // Add content
            Text title1 = new Text("The Strange Case of ").SetFontSize(12);
            Text title2 = new Text("Dr. Jekyll and Mr. Hyde").SetFontSize(16);
            Text author = new Text("Robert Louis Stevenson");

            Paragraph p = new Paragraph().SetFontSize(8)
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
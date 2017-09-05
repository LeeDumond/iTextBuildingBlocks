using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter03
{
    public class C03E07_TextExample
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter03\jekyll_hyde_text.pdf";

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

            Text t1 = new Text("The Strange Case of ");
            Text t2 = new Text("Dr. Jekyll").SetTextRise(5);
            Text t3 = new Text(" and ").SetHorizontalScaling(2);
            Text t4 = new Text("Mr. Hyde").SetSkew(10, 45);

            document.Add(
                new Paragraph(t1)
                    .Add(t2)
                    .Add(t3)
                    .Add(t4));

            document.Close();
        }
    }
}
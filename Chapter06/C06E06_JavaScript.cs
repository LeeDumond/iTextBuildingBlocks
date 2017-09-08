using System.IO;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter06
{
    public class C06E06_JavaScript
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter06\jekyll_hyde_javascript.pdf";

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
            Document document = new Document(pdf);

            Link link = new Link("here",
                PdfAction.CreateJavaScript("app.alert('Boo!');"));

            Paragraph p = new Paragraph()
                .Add("Click ")
                .Add(link.SetFontColor(Color.BLUE))
                .Add(" if you want to be scared.");

            document.Add(p);

            document.Close();
        }
    }
}
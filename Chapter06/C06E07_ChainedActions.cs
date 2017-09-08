using System.IO;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter06
{
    public class C06E07_ChainedActions
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter06\jekyll_hyde_chained.pdf";

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

            PdfAction action = PdfAction.CreateJavaScript("app.alert('Boo!');");
            action.Next(PdfAction.CreateGoToR(new FileInfo(C06E04_TOC_GoToNamed.DEST).FullName,1, true ));

            Link link = new Link("here", action);

            Paragraph p = new Paragraph()
                .Add("Click ")
                .Add(link.SetFontColor(Color.BLUE))
                .Add(" if you want to be scared.");

            document.Add(p);

            document.Close();
        }
    }
}
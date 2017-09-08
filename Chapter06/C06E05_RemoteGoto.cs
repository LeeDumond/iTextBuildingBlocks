using System.IO;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter06
{
    public class C06E05_RemoteGoto
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter06\jekyll_hyde_remote.pdf";

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

            Link link1 = new Link("Strange Case of Dr. Jekyll and Mr. Hyde",
                PdfAction.CreateGoToR(new FileInfo(C06E04_TOC_GoToNamed.DEST).FullName, 1, true));

            Link link2 = new Link("table of contents",
                PdfAction.CreateGoToR(new FileInfo(C06E04_TOC_GoToNamed.DEST).FullName, "toc", false));

            Paragraph p = new Paragraph()
                .Add("Read the amazing horror story ")
                .Add(link1.SetFontColor(Color.BLUE))
                .Add(" or, if you're too afraid to start reading the story, read the ")
                .Add(link2.SetFontColor(Color.BLUE))
                .Add(".");

            document.Add(p);

            document.Close();
        }
    }
}
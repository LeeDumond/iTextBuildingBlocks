using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Navigation;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iTextBuildingBlocks;

namespace Chapter06
{
    public class C06E08_ExplicitDestinations
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter06\jekyll_hyde_explicit.pdf";

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

            PdfDestination jekyll = PdfExplicitDestination.CreateFitH(1, 416);
            PdfDestination hyde = PdfExplicitDestination.CreateXYZ(1, 150, 516, 2);
            PdfDestination jekyll2 = PdfExplicitDestination.CreateFitR(2, 50, 380, 130, 440);

            document.Add(new Paragraph()
                .Add(new Link("Link to Dr. Jekyll", jekyll)));

            document.Add(new Paragraph()
                .Add(new Link("Link to Mr. Hyde", hyde)));

            document.Add(new Paragraph()
                .Add(new Link("Link to Dr. Jekyll on page 2", jekyll2)));

            document.Add(new Paragraph()
                .SetFixedPosition(50, 400, 80)
                .Add("Dr. Jekyll"));

            document.Add(new Paragraph()
                .SetFixedPosition(150, 500, 80)
                .Add("Mr. Hyde"));

            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

            document.Add(new Paragraph()
                .SetFixedPosition(50, 400, 80)
                .Add("Dr. Jekyll on page 2"));

            document.Close();
        }
    }
}
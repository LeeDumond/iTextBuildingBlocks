using System.IO;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter07
{
    public class C07E08_FullScreen
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter07\fullscreen.pdf";

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
            pdf.GetCatalog().SetPageMode(PdfName.FullScreen);
            PdfViewerPreferences preferences = new PdfViewerPreferences();
            preferences.SetNonFullScreenPageMode(PdfViewerPreferences.PdfViewerPreferencesConstants.USE_THUMBS);
            pdf.GetCatalog().SetViewerPreferences(preferences);
            Document document = new Document(pdf, PageSize.A8);
            document.Add(new Paragraph("Mr. Jekyl"));
            document.Add(new AreaBreak());
            document.Add(new Paragraph("Mr. Hyde"));
            document.Close();
        }
    }
}
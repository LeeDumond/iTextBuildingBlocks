using System.IO;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter07
{
    public class C07E09_ViewerPreferences
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter07\viewerpreferences.pdf";

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
            PdfViewerPreferences preferences = new PdfViewerPreferences();
            preferences.SetFitWindow(true);
            preferences.SetHideMenubar(true);
            preferences.SetHideToolbar(true);
            preferences.SetHideWindowUI(true);
            preferences.SetCenterWindow(true);
            preferences.SetDisplayDocTitle(true);
            pdf.GetCatalog().SetViewerPreferences(preferences);
            PdfDocumentInfo info = pdf.GetDocumentInfo();
            info.SetTitle("A Strange Case");
            Document document = new Document(pdf, PageSize.A4.Rotate());
            document.Add(new Paragraph("Mr. Jekyl and Mr. Hyde"));
            document.Close();
        }
    }
}
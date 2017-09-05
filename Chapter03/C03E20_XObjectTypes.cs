using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Wmf;
using iText.Kernel.Pdf.Xobject;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter03
{
    public class C03E20_XObjectTypes
    {
        private static readonly string WMF = $@"{Paths.ImageResourcesPath}\test\butterfly.wmf";
        private static readonly string SRC = $@"{Paths.PdfResourcesPath}\jekyll_hyde.pdf";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter03\xobject_types.pdf";

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

            PdfFormXObject xObject1 = new PdfFormXObject(new WmfImageData(WMF), pdf);
            Image img1 = new Image(xObject1);
            document.Add(img1);

            PdfReader reader = new PdfReader(SRC);
            PdfDocument existing = new PdfDocument(reader);
            PdfPage page = existing.GetPage(1);
            PdfFormXObject xObject2 = page.CopyAsFormXObject(pdf);
            Image img2 = new Image(xObject2);
            img2.ScaleToFit(400, 400);
            document.Add(img2);

            document.Close();
        }
    }
}
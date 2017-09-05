using System.IO;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iTextBuildingBlocks;

namespace Chapter03
{
    public class C03E14_MaryReillyV7
    {
        private static readonly string SRC = $@"{Paths.PdfResourcesPath}\jekyll_hyde.pdf";
        private static readonly string MARY = $@"{Paths.ImageResourcesPath}\0117002.jpg";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter03\mary_reilly_V7.pdf";

        public static void Main(string[] args)
        {
            FileInfo file = new FileInfo(DEST);
            if (file.Directory != null && !file.Directory.Exists)
            {
                file.Directory.Create();
            }

            ManipulatePdf(SRC, DEST);
        }

        private static void ManipulatePdf(string src, string dest)
        {
            PdfReader reader = new PdfReader(src);
            PdfWriter writer = new PdfWriter(dest);
            PdfDocument pdfDoc = new PdfDocument(reader, writer);

            Document document = new Document(pdfDoc);

            Image img = new Image(ImageDataFactory.Create(MARY));
            img.SetFixedPosition(1, 350, 750, UnitValue.CreatePointValue(50));
            document.Add(img);

            document.Close();
        }
    }
}
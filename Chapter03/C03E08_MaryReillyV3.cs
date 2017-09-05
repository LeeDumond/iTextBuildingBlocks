using System.IO;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter03
{
    public class C03E08_MaryReillyV3
    {
        private static readonly string MARY = $@"{Paths.ImageResourcesPath}\0117002.jpg";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter03\mary_reilly_V3.pdf";

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

            Paragraph p = new Paragraph(
                "Mary Reilly is a maid in the household of Dr. Jekyll: ");
            document.Add(p);

            Image img1 = new Image(ImageDataFactory.Create(MARY));
            document.Add(img1);
            Image img2 = new Image(ImageDataFactory.Create(MARY));
            document.Add(img2);

            document.Close();
        }
    }
}
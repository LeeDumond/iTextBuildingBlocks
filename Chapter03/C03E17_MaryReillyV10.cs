using System;
using System.IO;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter03
{
    public class C03E17_MaryReillyV10
    {
        private static readonly string MARY = $@"{Paths.ImageResourcesPath}\0117002.jpg";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter03\mary_reilly_V10.pdf";

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

            Paragraph p = new Paragraph(
                "Mary Reilly is a maid in the household of Dr. Jekyll: ");
            Image img = new Image(ImageDataFactory.Create(MARY));
            img.Scale(0.5f, 0.5f);
            img.SetRotationAngle(-Math.PI / 6);

            p.Add(img);
            document.Add(p);            

            document.Close();
        }
    }
}
using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iTextBuildingBlocks;

namespace Chapter02
{
    public class C02E14_ShowTextAligned
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter02\showtextaligned.pdf";

        public static void Main(string[] args)
        {
            var file = new FileInfo(DEST);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            CreatePdf(DEST);
        }

        private static void CreatePdf(string dest)
        {
            //Initialize PDF document
            var pdf = new PdfDocument(new PdfWriter(dest));

            // Initialize document
            var document = new Document(pdf);

            var title = new Paragraph("The Strange Case of Dr. Jekyll and Mr. Hyde");
            document.ShowTextAligned(title, 36, 806, TextAlignment.LEFT);

            var author = new Paragraph("by Robert Louis Stevenson");
            document.ShowTextAligned(author, 36, 806, TextAlignment.LEFT, VerticalAlignment.TOP);

            document.ShowTextAligned("Jekyll", 300, 800, TextAlignment.CENTER, 0.5f * (float) Math.PI);
            document.ShowTextAligned("Hyde", 300, 800, TextAlignment.CENTER, -0.5f * (float) Math.PI);

            document.ShowTextAligned("Jekyll", 350, 800, TextAlignment.CENTER, VerticalAlignment.TOP,
                0.5f * (float) Math.PI);
            document.ShowTextAligned("Hyde", 350, 800, TextAlignment.CENTER, VerticalAlignment.TOP,
                -0.5f * (float) Math.PI);

            document.ShowTextAligned("Jekyll", 400, 800, TextAlignment.CENTER, VerticalAlignment.MIDDLE,
                0.5f * (float) Math.PI);
            document.ShowTextAligned("Hyde", 400, 800, TextAlignment.CENTER, VerticalAlignment.MIDDLE,
                -0.5f * (float) Math.PI);

            document.Close();
        }
    }
}
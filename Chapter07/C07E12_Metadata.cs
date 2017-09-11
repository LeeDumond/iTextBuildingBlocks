using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter07
{
    public class C07E12_Metadata
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter07\metadata.pdf";

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
            PdfDocument pdf = new PdfDocument(
                new PdfWriter(dest,
                    new WriterProperties()
                        .AddXmpMetadata()
                        .SetPdfVersion(PdfVersion.PDF_1_6)));

            PdfDocumentInfo info = pdf.GetDocumentInfo();
            info.SetTitle("The Strange Case of Dr. Jekyll and Mr. Hyde");
            info.SetAuthor("Robert Louis Stevenson");
            info.SetSubject("A novel");
            info.SetKeywords("Dr. Jekyll, Mr. Hyde");
            info.SetCreator("A simple tutorial example");

            Document document = new Document(pdf);

            document.Add(new Paragraph("Mr. Jekyl and Mr. Hyde"));

            document.Close();
        }
    }
}
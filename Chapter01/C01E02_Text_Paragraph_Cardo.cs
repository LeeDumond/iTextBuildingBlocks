using System.IO;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter01
{
    public class C01E02_Text_Paragraph_Cardo
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter01\text_paragraph_cardo.pdf";

        private static readonly string REGULAR = $@"{Paths.FontResourcesPath}\Cardo-Regular.ttf";
        private static readonly string BOLD = $@"{Paths.FontResourcesPath}\Cardo-Bold.ttf";
        private static readonly string ITALIC = $@"{Paths.FontResourcesPath}\Cardo-Italic.ttf";

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

            // Initialize document
            Document document = new Document(pdf);

            // Add content
            PdfFont font = PdfFontFactory.CreateFont(REGULAR, true);
            PdfFont bold = PdfFontFactory.CreateFont(BOLD, true);
            PdfFont italic = PdfFontFactory.CreateFont(ITALIC, true);
            Text title = new Text("The Strange Case of Dr. Jekyll and Mr. Hyde").SetFont(bold);
            Text author = new Text("Robert Louis Stevenson").SetFont(font);
            Paragraph p = new Paragraph().SetFont(italic).Add(title).Add(" by ").Add(author);
            document.Add(p);

            //Close document
            document.Close();
        }
    }
}
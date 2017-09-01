using System.IO;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Hyphenation;
using iText.Layout.Properties;
using iTextBuildingBlocks;

namespace Chapter02
{
    public class C02E09_JekyllHydeV5
    {
        private static readonly string SRC = $@"{Paths.TextResourcesPath}\jekyll_hyde.txt";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter02\jekyll_hyde_v5.pdf";

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
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));

            //Initialize document
            Document document = new Document(pdf);

            //Set column parameters
            float offSet = 36;
            float gutter = 23;
            float columnWidth = (PageSize.A4.GetWidth() - offSet * 2) / 2 - gutter;
            float columnHeight = PageSize.A4.GetHeight() - offSet * 2;

            //Define column areas
            Rectangle[] columns = {
                new Rectangle(offSet, offSet, columnWidth, columnHeight),
                new Rectangle(offSet + columnWidth + gutter, offSet, columnWidth, columnHeight)};

            document.SetRenderer(new ColumnDocumentRenderer(document, columns));

            PdfFont font = PdfFontFactory.CreateFont(FontConstants.TIMES_ROMAN);
            PdfFont bold = PdfFontFactory.CreateFont(FontConstants.HELVETICA_BOLD);

            document
                .SetTextAlignment(TextAlignment.JUSTIFIED)
                .SetFont(font)
                .SetHyphenation(new HyphenationConfig("en", "uk", 3, 3));
                
            StreamReader sr = File.OpenText(SRC);
            string line;
            Paragraph p;
            bool title = true;
            AreaBreak nextPage = new AreaBreak(AreaBreakType.NEXT_PAGE);

            while ((line = sr.ReadLine()) != null)
            {
                p = new Paragraph(line);
                p.SetKeepTogether(true);

                if (title)
                {
                    p.SetFont(bold).SetFontSize(12);
                    title = false;
                }
                else
                {
                    p.SetFirstLineIndent(36);
                }

                if (string.IsNullOrWhiteSpace(line))
                {
                    document.Add(nextPage);
                    title = true;
                }

                document.Add(p);
            }

            //Close document
            document.Close();
        }
    }
}
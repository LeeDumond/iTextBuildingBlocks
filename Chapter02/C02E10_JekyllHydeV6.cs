using System.IO;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Hyphenation;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using iTextBuildingBlocks;

namespace Chapter02
{
    public class C02E10_JekyllHydeV6
    {
        private static readonly string SRC = $@"{Paths.TextResourcesPath}\jekyll_hyde.txt";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter02\jekyll_hyde_v6.pdf";

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

            Paragraph p = new Paragraph()
                .Add("Be prepared to read a story about a London lawyer "
                     + "named Gabriel John Utterson who investigates strange "
                     + "occurrences between his old friend, Dr. Henry Jekyll, "
                     + "and the evil Edward Hyde.");

            document.Add(p);
            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

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
            document.Add(new AreaBreak(AreaBreakType.LAST_PAGE));

            PdfFont font = PdfFontFactory.CreateFont(FontConstants.TIMES_ROMAN);
            PdfFont bold = PdfFontFactory.CreateFont(FontConstants.HELVETICA_BOLD);

            document
                .SetTextAlignment(TextAlignment.JUSTIFIED)
                .SetFont(font)
                .SetHyphenation(new HyphenationConfig("en", "uk", 3, 3));                

            StreamReader sr = File.OpenText(SRC);
            string line;
            bool title = true;
            AreaBreak nextArea = new AreaBreak(AreaBreakType.NEXT_AREA);

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
                    document.Add(nextArea);
                    title = true;
                }

                document.Add(p);
            }

            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));
            document.SetRenderer(new DocumentRenderer(document));
            document.Add(new AreaBreak(AreaBreakType.LAST_PAGE));
            p = new Paragraph()
                .Add("This was the story about the London lawyer "
                     + "named Gabriel John Utterson who investigates strange "
                     + "occurrences between his old friend, Dr. Henry Jekyll, "
                     + "and the evil Edward Hyde. THE END!");
            document.Add(p);

            //Close document
            document.Close();
        }
    }
}
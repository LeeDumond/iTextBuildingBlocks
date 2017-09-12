using System.IO;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Navigation;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Hyphenation;
using iText.Layout.Properties;
using iTextBuildingBlocks;

namespace Chapter06
{
    public class C06E10_TOC_OutlinesNames
    {
        private static readonly string SRC = $@"{Paths.TextResourcesPath}\jekyll_hyde.txt";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter06\jekyll_hyde_outline1.pdf";

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
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            pdf.GetCatalog().SetPageMode(PdfName.UseOutlines);

            // Initialize document
            Document document = new Document(pdf);

            PdfFont font = PdfFontFactory.CreateFont(FontConstants.TIMES_ROMAN);
            PdfFont bold = PdfFontFactory.CreateFont(FontConstants.HELVETICA_BOLD);

            document.SetTextAlignment(TextAlignment.JUSTIFIED)
                .SetHyphenation(new HyphenationConfig("en", "uk", 3, 3))
                .SetFont(font)
                .SetFontSize(11);

            StreamReader sr = File.OpenText(SRC);
            string line;
            bool title = true;
            int counter = 0;
            PdfOutline outline = null;

            while ((line = sr.ReadLine()) != null)
            {
                Paragraph p = new Paragraph(line);
                p.SetKeepTogether(true);
                if (title)
                {
                    string name = $"title{counter++:D2}";
                    outline = CreateOutline(outline, pdf, line, name);
                    p.SetFont(bold).SetFontSize(12)
                        .SetKeepWithNext(true)
                        .SetDestination(name);
                    title = false;
                    document.Add(p);
                }
                else
                {
                    p.SetFirstLineIndent(36);
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        p.SetMarginBottom(12);
                        title = true;
                    }
                    else
                    {
                        p.SetMarginBottom(0);
                    }

                    document.Add(p);
                }
            }

            //Close document
            document.Close();
        }

        private static PdfOutline CreateOutline(PdfOutline outline, PdfDocument pdf, string title, string name)
        {
            if (outline == null)
            {
                outline = pdf.GetOutlines(false);
                outline = outline.AddOutline(title);
                outline.AddDestination(PdfDestination.MakeDestination(new PdfString(name)));

                return outline;
            }

            PdfOutline kid = outline.AddOutline(title);
            kid.AddDestination(PdfDestination.MakeDestination(new PdfString(name)));

            return outline;
        }
    }
}
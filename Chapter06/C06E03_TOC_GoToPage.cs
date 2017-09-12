using System.Collections.Generic;
using System.IO;
using System.Linq;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Pdf.Navigation;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Hyphenation;
using iText.Layout.Properties;
using iTextBuildingBlocks;

namespace Chapter06
{
    public class C06E03_TOC_GoToPage
    {
        private static readonly string SRC = $@"{Paths.TextResourcesPath}\jekyll_hyde.txt";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter06\jekyll_hyde_toc1.pdf";

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
            Paragraph p;
            bool title = true;
            int counter = 0;

            Dictionary<string, int> toc = new Dictionary<string, int>();

            while ((line = sr.ReadLine()) != null)
            {
                p = new Paragraph(line);
                p.SetKeepTogether(true);
                if (title)
                {
                    string name = $"title{counter++:D2}";
                    p.SetFont(bold).SetFontSize(12)
                        .SetKeepWithNext(true)
                        .SetDestination(name);
                    title = false;
                    document.Add(p);

                    // The following line is problematic when using setKeepWithNext
                    toc.Add(line, pdf.GetNumberOfPages());
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

            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

            p = new Paragraph().SetFont(bold).Add("Table of Contents");
            document.Add(p);

            string firstKey = toc.Select(d => d.Key).First();
            toc.Remove(firstKey);

            List<TabStop> tabstops = new List<TabStop> {new TabStop(580, TabAlignment.RIGHT, new DottedLine())};
            foreach (KeyValuePair<string, int> entry in toc)
            {
                p = new Paragraph()
                    .AddTabStops(tabstops)
                    .Add(entry.Key)
                    .Add(new Tab())
                    .Add(entry.Value.ToString())
                    .SetAction(PdfAction.CreateGoTo(PdfExplicitDestination.CreateFit(entry.Value)));

                document.Add(p);
            }

            //Close document
            document.Close();
        }
    }
}
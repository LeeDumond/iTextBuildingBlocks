using System.Collections.Generic;
using System.IO;
using System.Linq;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Hyphenation;
using iText.Layout.Properties;
using iTextBuildingBlocks;

namespace Chapter07
{
    public class C07E06_PageLabels
    {
        private static readonly string SRC = $@"{Paths.TextResourcesPath}\jekyll_hyde.txt";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter07\pagelabels.pdf";

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
            PdfPage page = pdf.AddNewPage();
            page.SetPageLabel(
                (PageLabelNumberingStyleConstants?) PageLabelNumberingStyleConstants.LOWERCASE_ROMAN_NUMERALS, null);

            Document document = new Document(pdf);
            document.Add(new Paragraph().Add("Page left blank intentionally"));
            document.Add(new AreaBreak());
            document.Add(new Paragraph().Add("Page left blank intentionally"));
            document.Add(new AreaBreak());
            document.Add(new Paragraph().Add("Page left blank intentionally"));
            document.Add(new AreaBreak());
            page = pdf.GetLastPage();
            page.SetPageLabel(
                (PageLabelNumberingStyleConstants?) PageLabelNumberingStyleConstants.DECIMAL_ARABIC_NUMERALS, null, 1);
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

            Dictionary<string, KeyValuePair<string, int>> toc = new Dictionary<string, KeyValuePair<string, int>>();

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
                    toc.Add(name, new KeyValuePair<string, int>(line, pdf.GetNumberOfPages()));
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

            p = new Paragraph().SetFont(bold)
                .Add("Table of Contents").SetDestination("toc");
            document.Add(p);
            page = pdf.GetLastPage();
            page.SetPageLabel(null, "TOC", 1);

            string firstKey = toc.Select(t => t.Key).First();
            toc.Remove(firstKey);
            List<TabStop> tabstops = new List<TabStop> {new TabStop(580, TabAlignment.RIGHT, new DottedLine())};
            foreach (KeyValuePair<string, KeyValuePair<string, int>> entry in toc)
            {
                KeyValuePair<string, int> text = entry.Value;
                p = new Paragraph()
                    .AddTabStops(tabstops)
                    .Add(text.Key)
                    .Add(new Tab())
                    .Add(text.Value.ToString())
                    .SetAction(PdfAction.CreateGoTo(entry.Key));
                document.Add(p);
            }

            document.Close();
        }
    }
}
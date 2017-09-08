using System;
using System.Collections;
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
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using iTextBuildingBlocks;

namespace Chapter06
{
    public class C06E04_TOC_GoToNamed
    {
        private static readonly string SRC = $@"{Paths.TextResourcesPath}\jekyll_hyde.txt";
        internal static readonly string DEST = $@"{Paths.ResultsPath}\chapter06\jekyll_hyde_toc2.pdf";

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

            Dictionary<string, KeyValuePair<string, int>> toc = new Dictionary<string, KeyValuePair<string, int>>();

            while ((line = sr.ReadLine()) != null)
            {
                p = new Paragraph(line);
                p.SetKeepTogether(true);
                if (title)
                {
                    string name = $"title{counter++:D2}";
                    KeyValuePair<string, int> titlePage = new KeyValuePair<string, int>(line, pdf.GetNumberOfPages());

                    p.SetFont(bold).SetFontSize(12)
                        .SetKeepWithNext(true)
                        .SetDestination(name)
                        .SetNextRenderer(new UpdatePageRenderer(p, titlePage));
                    title = false;
                    document.Add(p);
                    toc.Add(name, titlePage);
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

            string firstKey = toc.Select(d => d.Key).First();
            toc.Remove(firstKey);

            List<TabStop> tabstops = new List<TabStop>();
            tabstops.Add(new TabStop(580, TabAlignment.RIGHT, new DottedLine()));

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

            //Close document
            document.Close();
        }

        private class UpdatePageRenderer : ParagraphRenderer
        {
            private KeyValuePair<string, int> entry;

            public UpdatePageRenderer(Paragraph modelElement, KeyValuePair<string, int> entry) : base(modelElement)
            {
                this.entry = entry;
            }

            public override LayoutResult Layout(LayoutContext layoutContext)
            {
                LayoutResult result = base.Layout(layoutContext);
                entry = new KeyValuePair<string, int>(entry.Key, layoutContext.GetArea().GetPageNumber());

                return result;
            }
        }
    }
}
using System;
using System.IO;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Navigation;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Hyphenation;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using iTextBuildingBlocks;

namespace Chapter06
{
    public class C06E11_TOC_OutlinesDestinations
    {
        private static readonly string SRC = $@"{Paths.TextResourcesPath}\jekyll_hyde.txt";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter06\jekyll_hyde_outline2.pdf";

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
            Paragraph p;
            bool title = true;
            int counter = 0;
            PdfOutline outline = null;

            while ((line = sr.ReadLine()) != null)
            {
                p = new Paragraph(line);
                p.SetKeepTogether(true);
                if (title)
                {
                    string name = $"title{counter++:D2}";
                    outline = CreateOutline(outline, pdf, line, p);
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

        private static PdfOutline CreateOutline(PdfOutline outline, PdfDocument pdf, string title, Paragraph p)
        {
            if (outline == null)
            {
                outline = pdf.GetOutlines(false);
                outline = outline.AddOutline(title);

                return outline;
            }

            OutlineRenderer renderer = new OutlineRenderer(p, title, outline);
            p.SetNextRenderer(renderer);
            return outline;

        }

        private class OutlineRenderer : ParagraphRenderer
        {
            private readonly PdfOutline parentOutline;
            private readonly string title;

            public OutlineRenderer(Paragraph modelElement, string title, PdfOutline parent) : base(modelElement)
            {
                this.title = title;
                this.parentOutline = parent;
            }

            public override void Draw(DrawContext drawContext)
            {
                base.Draw(drawContext);

                Rectangle rect = GetOccupiedAreaBBox();
                PdfDestination dest = PdfExplicitDestination.CreateXYZ(drawContext.GetDocument().GetLastPage(),
                    rect.GetLeft(), rect.GetTop(), 0);

                PdfOutline outline = parentOutline.AddOutline(title);
                outline.AddDestination(dest);
            }
        }
    }
}
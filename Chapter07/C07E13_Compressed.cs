using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Events;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Kernel.Pdf.Extgstate;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Hyphenation;
using iText.Layout.Properties;
using iTextBuildingBlocks;

namespace Chapter07
{
    public class C07E13_Compressed
    {
        private static readonly string SRC = $@"{Paths.TextResourcesPath}\jekyll_hyde.txt";
        private static readonly string IMG = $@"{Paths.ImageResourcesPath}\3132614.jpg";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter07\jekyll_hyde_compressed.pdf";

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
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest, new WriterProperties().SetFullCompressionMode(true)));
            Image img = new Image(ImageDataFactory.Create(IMG));
            IEventHandler handler = new TransparentImage(img);
            pdf.AddEventHandler(PdfDocumentEvent.START_PAGE, handler);
            // Initialize document
            Document document = new Document(pdf);
            PdfFont bold = PdfFontFactory.CreateFont(FontConstants.HELVETICA_BOLD);
            document.SetTextAlignment(TextAlignment.JUSTIFIED)
                .SetHyphenation(new HyphenationConfig("en", "uk", 3, 3));

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

            pdf.RemoveEventHandler(PdfDocumentEvent.START_PAGE, handler);
            document.Add(new AreaBreak(AreaBreakType.NEXT_PAGE));

            p = new Paragraph().SetFont(bold)
                .Add("Table of Contents").SetDestination("toc");
            document.Add(p);

            string firstKey = toc.Select(t => t.Key).First();
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

        private class TransparentImage : IEventHandler
        {
            protected PdfExtGState gState;
            protected Image img;

            public TransparentImage(Image img)
            {
                this.img = img;
                gState = new PdfExtGState().SetFillOpacity(0.2f);
            }

            public void HandleEvent(Event @event)
            {
                PdfDocumentEvent docEvent = (PdfDocumentEvent) @event;
                PdfDocument pdf = docEvent.GetDocument();
                PdfPage page = docEvent.GetPage();
                Rectangle pageSize = page.GetPageSize();
                PdfCanvas pdfCanvas = new PdfCanvas(page.NewContentStreamBefore(), page.GetResources(), pdf);
                pdfCanvas.SaveState().SetExtGState(gState);
                Canvas canvas = new Canvas(pdfCanvas, pdf, page.GetPageSize());
                canvas.Add(img.ScaleAbsolute(pageSize.GetWidth(), pageSize.GetHeight()));
                pdfCanvas.RestoreState();
                pdfCanvas.Release();
            }
        }
    }
}
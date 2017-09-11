using System.Collections.Generic;
using System.IO;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Hyphenation;
using iText.Layout.Layout;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using iTextBuildingBlocks;

namespace Chapter02
{
    public class C02E11_JekyllHydeV7
    {
        private static readonly string SRC = $@"{Paths.TextResourcesPath}\jekyll_hyde.txt";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter02\jekyll_hyde_v7.pdf";

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

            //Set column parameters
            float offSet = 36;
            float gutter = 23;
            float columnWidth = (PageSize.A4.GetWidth() - offSet * 2) / 2 - gutter;
            float columnHeight = PageSize.A4.GetHeight() - offSet * 2;

            //Define column areas
            Rectangle[] columns =
            {
                new Rectangle(offSet, offSet, columnWidth, columnHeight),
                new Rectangle(offSet + columnWidth + gutter, offSet, columnWidth, columnHeight)
            };
            DocumentRenderer renderer = new MyColumnRenderer(document, columns);
            document.SetRenderer(renderer);

            PdfFont font = PdfFontFactory.CreateFont(FontConstants.TIMES_ROMAN);
            PdfFont bold = PdfFontFactory.CreateFont(FontConstants.HELVETICA_BOLD);
            document.SetTextAlignment(TextAlignment.JUSTIFIED)
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
            renderer.Flush();
            document.Close();
        }

        private class MyColumnRenderer : DocumentRenderer
        {
            private int _nextAreaNumber;
            private readonly Rectangle[] _columns;
            private int _currentAreaNumber;
            private readonly HashSet<int> _moveColumn = new HashSet<int>();

            public MyColumnRenderer(Document document, Rectangle[] columns) : base(document, false)
            {
                _columns = columns;
            }

            protected override LayoutArea UpdateCurrentArea(LayoutResult overflowResult)
            {
                if (overflowResult != null && overflowResult.GetAreaBreak() != null &&
                    overflowResult.GetAreaBreak().GetAreaType() != AreaBreakType.NEXT_AREA)
                {
                    _nextAreaNumber = 0;
                }

                if (_nextAreaNumber % _columns.Length == 0)
                {
                    base.UpdateCurrentArea(overflowResult);
                }

                _currentAreaNumber = _nextAreaNumber + 1;

                return (currentArea = new LayoutArea(currentPageNumber,
                    _columns[_nextAreaNumber++ % _columns.Length].Clone()));
            }

            protected override PageSize AddNewPage(PageSize customPageSize)
            {
                if (_currentAreaNumber != _nextAreaNumber
                    && _currentAreaNumber % _columns.Length != 0)
                {
                    _moveColumn.Add(currentPageNumber - 1);
                }
                    
                return base.AddNewPage(customPageSize);
            }

            protected override void FlushSingleRenderer(IRenderer resultRenderer)
            {
                int pageNum = resultRenderer.GetOccupiedArea().GetPageNumber();
                if (_moveColumn.Contains(pageNum))
                {
                    resultRenderer.Move(_columns[0].GetWidth() / 2, 0);
                }

                base.FlushSingleRenderer(resultRenderer);
            }
        }
    }
}
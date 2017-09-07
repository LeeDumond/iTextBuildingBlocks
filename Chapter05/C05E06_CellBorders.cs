using System.IO;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Layout.Renderer;
using iTextBuildingBlocks;

namespace Chapter05
{
    public class C05E06_CellBorders
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter05\cell_borders.pdf";

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

            Table table1 = new Table(new float[] { 2, 1, 1 });
            table1.SetWidthPercent(80);
            table1.SetHorizontalAlignment(HorizontalAlignment.CENTER);

            table1.AddCell(
                new Cell(1, 3).Add("Cell with colspan 3")
                    .SetPadding(10).SetMargin(5).SetBorder(new DashedBorder(0.5f)));

            table1.AddCell(new Cell(2, 1).Add("Cell with rowspan 2")
                .SetMarginTop(5).SetMarginBottom(5)
                .SetBorderBottom(new DottedBorder(0.5f))
                .SetBorderLeft(new DottedBorder(0.5f)));

            table1.AddCell(new Cell().Add("row 1; cell 1")
                .SetBorder(new DottedBorder(Color.ORANGE, 0.5f)));

            table1.AddCell(new Cell().Add("row 1; cell 2"));

            table1.AddCell(new Cell().Add("row 2; cell 1").SetMargin(10)
                .SetBorderBottom(new SolidBorder(2)));

            table1.AddCell(new Cell().Add("row 2; cell 2").SetPadding(10)
                .SetBorderBottom(new SolidBorder(2)));

            document.Add(table1);

            Table table2 = new Table(new float[] { 2, 1, 1 });
            table2.SetMarginTop(10);
            table2.SetBorder(new SolidBorder(1));
            table2.SetWidthPercent(80);
            table2.SetHorizontalAlignment(HorizontalAlignment.CENTER);

            table2.AddCell(new Cell(1, 3)
                .Add("Cell with colspan 3").SetBorder(Border.NO_BORDER));

            table2.AddCell(new Cell(2, 1)
                .Add("Cell with rowspan 2").SetBorder(Border.NO_BORDER));

            table2.AddCell(new Cell()
                .Add("row 1; cell 1").SetBorder(Border.NO_BORDER));

            table2.AddCell(new Cell()
                .Add("row 1; cell 2").SetBorder(Border.NO_BORDER));

            table2.AddCell(new Cell()
                .Add("row 2; cell 1").SetBorder(Border.NO_BORDER));

            table2.AddCell(new Cell()
                .Add("row 2; cell 2").SetBorder(Border.NO_BORDER));

            document.Add(table2);

            Table table3 = new Table(new float[] { 2, 1, 1 });
            table3.SetMarginTop(10);
            table3.SetWidthPercent(80);
            table3.SetHorizontalAlignment(HorizontalAlignment.CENTER);

            Cell cell = new RoundedCornersCell(1, 3).Add("Cell with colspan 3")
                .SetPadding(10).SetMargin(5).SetBorder(Border.NO_BORDER);
            table3.AddCell(cell);

            cell = new RoundedCornersCell(2, 1).Add("Cell with rowspan 2")
                .SetMarginTop(5).SetMarginBottom(5);
            table3.AddCell(cell);

            cell = new RoundedCornersCell().Add("row 1; cell 1");
            table3.AddCell(cell);

            cell = new RoundedCornersCell().Add("row 1; cell 2");
            table3.AddCell(cell);

            cell = new RoundedCornersCell().Add("row 2; cell 1").SetMargin(10);
            table3.AddCell(cell);

            cell = new RoundedCornersCell().Add("row 2; cell 2").SetPadding(10);
            table3.AddCell(cell);

            document.Add(table3);

            document.Close();
        }

        private class RoundedCornersCellRenderer : CellRenderer
        {
            public RoundedCornersCellRenderer(Cell modelElement) : base(modelElement)
            {
            }

            public override void DrawBorder(DrawContext drawContext)
            {
                Rectangle occupiedAreaBBox = GetOccupiedAreaBBox();
                float[] margins = GetMargins();
                Rectangle rectangle = ApplyMargins(occupiedAreaBBox, margins, false);
                PdfCanvas canvas = drawContext.GetCanvas();
                canvas.RoundRectangle(rectangle.GetX() + 1, rectangle.GetY() + 1,
                    rectangle.GetWidth() - 2, rectangle.GetHeight() - 2, 5)
                    .Stroke();

                base.DrawBorder(drawContext);
            }
        }

        private class RoundedCornersCell : Cell
        {
            public RoundedCornersCell()
            {
                
            }

            public RoundedCornersCell(int rowspan, int colspan) : base(rowspan, colspan)
            {
                
            }

            protected override IRenderer MakeNewRenderer()
            {
                return new RoundedCornersCellRenderer(this);
            }
        }
    }
}
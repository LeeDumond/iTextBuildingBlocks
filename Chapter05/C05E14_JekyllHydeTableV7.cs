using System.Collections.Generic;
using System.IO;
using iText.Kernel.Colors;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Renderer;
using iTextBuildingBlocks;

namespace Chapter05
{
    public class C05E14_JekyllHydeTableV7
    {
        private static readonly string SRC = $@"{Paths.DataResourcesPath}\jekyll_hyde.csv";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter05\jekyll_hyde_table7.pdf";

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
            Document document = new Document(pdf, PageSize.A4.Rotate());

            Table table = new Table(new float[] { 3, 2, 14, 9, 4, 3 });
            table.SetWidthPercent(100);

            List<List<string>> resultSet = CsvTo2DList.Convert(SRC, "|");
            List<string> header = resultSet[0];
            resultSet.RemoveAt(0);

            foreach (string field in header)
            {
                table.AddHeaderCell(field);
            }

            foreach (List<string> record in resultSet)
            {
                table.AddCell(record[0]);
                table.AddCell(record[1]);
                Cell cell = new Cell().Add(record[2]);
                cell.SetNextRenderer(new RunlengthRenderer(cell, record[5]));
                table.AddCell(cell);
                table.AddCell(record[3]);
                table.AddCell(record[4]);
                table.AddCell(record[5]);
            }

            document.Add(table);

            document.Close();
        }

        private class RunlengthRenderer : CellRenderer
        {
            private int runlength;

            public RunlengthRenderer(Cell modelElement, string duration) : base(modelElement)
            {
                runlength = string.IsNullOrWhiteSpace(duration) ? 0 : int.Parse(duration);
            }

            public override IRenderer GetNextRenderer()
            {
                return new RunlengthRenderer((Cell) GetModelElement(), runlength.ToString());
            }

            public override void DrawBackground(DrawContext drawContext)
            {
                if (runlength == 0)
                {
                    return;
                }

                PdfCanvas canvas = drawContext.GetCanvas();
                canvas.SaveState();
                if (runlength < 90)
                {
                    canvas.SetFillColor(Color.GREEN);
                }
                else if (runlength > 240)
                {
                    runlength = 240;
                    canvas.SetFillColor(Color.RED);
                }
                else
                {
                    canvas.SetFillColor(Color.ORANGE);
                }
                Rectangle rect = GetOccupiedAreaBBox();
                canvas.Rectangle(rect.GetLeft(), rect.GetBottom(),
                    rect.GetWidth() * runlength / 240, rect.GetHeight());
                canvas.Fill();
                canvas.RestoreState();

                base.DrawBackground(drawContext);
            }
        }
    }
}
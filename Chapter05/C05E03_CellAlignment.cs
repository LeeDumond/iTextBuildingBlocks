using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iTextBuildingBlocks;

namespace Chapter05
{
    public class C05E03_CellAlignment
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter05\cell_alignment.pdf";

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

            Table table = new Table(new float[] { 2, 1, 1 });
            table.SetWidthPercent(80);
            table.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            table.SetTextAlignment(TextAlignment.CENTER);

            table.AddCell(new Cell(1, 3).Add("Cell with colspan 3"));
            table.AddCell(new Cell(2, 1).Add("Cell with rowspan 2")
                .SetTextAlignment(TextAlignment.RIGHT));
            table.AddCell("row 1; cell 1");
            table.AddCell("row 1; cell 2");
            table.AddCell("row 2; cell 1");
            table.AddCell("row 2; cell 2");

            Cell cell = new Cell()
                .Add(new Paragraph("Left").SetTextAlignment(TextAlignment.LEFT))
                .Add(new Paragraph("Center"))
                .Add(new Paragraph("Right").SetTextAlignment(TextAlignment.RIGHT));
            table.AddCell(cell);

            cell = new Cell().Add("Middle")
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            table.AddCell(cell);

            cell = new Cell().Add("Bottom")
                .SetVerticalAlignment(VerticalAlignment.BOTTOM);
            table.AddCell(cell);

            document.Add(table);

            document.Close();;
        }
    }
}
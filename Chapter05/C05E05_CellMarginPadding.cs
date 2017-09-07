using System.IO;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iTextBuildingBlocks;

namespace Chapter05
{
    public class C05E05_CellMarginPadding
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter05\cell_margin_padding.pdf";

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

            Table table = new Table(new float[] {2, 1, 1});
            table.SetBackgroundColor(Color.ORANGE);
            table.SetWidthPercent(80);
            table.SetHorizontalAlignment(HorizontalAlignment.CENTER);

            table.AddCell(new Cell(1, 3).Add("Cell with colspan 3")
                .SetPadding(10).SetMargin(5).SetBackgroundColor(Color.GREEN));

            table.AddCell(new Cell(2, 1).Add("Cell with rowspan 2")
                .SetMarginTop(5).SetMarginBottom(5).SetPaddingLeft(30)
                .SetFontColor(Color.WHITE).SetBackgroundColor(Color.BLUE));

            table.AddCell(new Cell().Add("row 1; cell 1")
                .SetFontColor(Color.WHITE).SetBackgroundColor(Color.RED));

            table.AddCell(new Cell().Add("row 1; cell 2"));

            table.AddCell(new Cell().Add("row 2; cell 1").SetMargin(10)
                .SetFontColor(Color.WHITE).SetBackgroundColor(Color.RED));

            table.AddCell(new Cell().Add("row 2; cell 2").SetPadding(10)
                .SetFontColor(Color.WHITE).SetBackgroundColor(Color.RED));

            document.Add(table);

            document.Close();
        }
    }
}
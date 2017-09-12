using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iTextBuildingBlocks;

namespace Chapter05
{
    public class C05E07_NestedTable
    {

private static readonly string DEST = $@"{Paths.ResultsPath}\chapter05\nested_tables.pdf";

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

            Table table = new Table(new []{1f, 1f});
            table.SetWidthPercent(80);
            table.SetHorizontalAlignment(HorizontalAlignment.CENTER);

            table.AddCell(new Cell(1, 2).Add("Cell with colspan 2"));
            table.AddCell(new Cell().Add("Cell with rowspan 1"));

            Table inner = new Table(new[] { 1f, 1f });
            inner.AddCell("row 1; cell 1");
            inner.AddCell("row 1; cell 2");
            inner.AddCell("row 2; cell 1");
            inner.AddCell("row 2; cell 2");

            table.AddCell(inner);

            document.Add(table);

            table = new Table(new[] { 1f, 1f });
            table.SetMarginTop(10);
            table.SetWidthPercent(80);
            table.SetHorizontalAlignment(HorizontalAlignment.CENTER);

            table.AddCell(new Cell(1, 2).Add("Cell with colspan 2"));
            table.AddCell(new Cell().Add("Cell with rowspan 1"));

            inner = new Table(new[] { 1f, 1f });
            inner.AddCell("row 1; cell 1");
            inner.AddCell("row 1; cell 2");
            inner.AddCell("row 2; cell 1");
            inner.AddCell("row 2; cell 2");

            table.AddCell(new Cell().Add(inner).SetPadding(0));

            document.Add(table);

            document.Close();
        }
    }
}
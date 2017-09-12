using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter05
{
    public class C05E01_MyFirstTable
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter05\my_first_table.pdf";

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

            Table table = new Table(new []{1f,1f,1f});
            table.AddCell(new Cell(1, 3).Add("Cell with colspan 3"));
            table.AddCell(new Cell(2, 1).Add("Cell with rowspan 2"));
            table.AddCell("row 1; cell 1");
            table.AddCell("row 1; cell 2");
            table.AddCell("row 2; cell 1");
            table.AddCell("row 2; cell 2");

            document.Add(table);

            document.Close();
        }
    }
}
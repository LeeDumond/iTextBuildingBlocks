using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter05
{
    public class C05E15_LargeTable
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter05\large_table.pdf";

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

            Table table = new Table(new[] {1f, 1f, 1f}, true);

            table.AddHeaderCell("Table header 1");
            table.AddHeaderCell("Table header 2");
            table.AddHeaderCell("Table header 3");
            table.AddFooterCell("Table footer 1");
            table.AddFooterCell("Table footer 2");
            table.AddFooterCell("Table footer 3");

            document.Add(table);

            for (int i = 0; i < 1000; i++)
            {
                table.AddCell($"Row {i + 1}; column 1");
                table.AddCell($"Row {i + 1}; column 2");
                table.AddCell($"Row {i + 1}; column 3");

                if (i % 50 == 0)
                {
                    table.Flush();
                }
            }

            table.Complete();

            document.Close();
        }
    }
}
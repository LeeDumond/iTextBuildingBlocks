using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter05
{
    public class C05E04_ColumnHeights
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter05\column_heights.pdf";

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

            Paragraph p =
                new Paragraph("The Strange Case of\nDr. Jekyll\nand\nMr. Hyde")
                    .SetBorder(new DashedBorder(0.3f));

            // Initialize document
            Document document = new Document(pdf);

            Table table = new Table(1);

            table.AddCell(p);

            Cell cell = new Cell().SetHeight(16).Add(p);
            table.AddCell(cell);

            cell = new Cell().SetHeight(144).Add(p);
            table.AddCell(cell);

            cell = new Cell().Add(p).SetRotationAngle(Math.PI / 6);
            table.AddCell(cell);

            document.Add(table);

            document.Close();
        }
    }
}
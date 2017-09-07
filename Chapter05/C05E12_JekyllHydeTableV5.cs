using System;
using System.Collections.Generic;
using System.IO;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter05
{
    public class C05E12_JekyllHydeTableV5
    {
        private static readonly string SRC = $@"{Paths.DataResourcesPath}\jekyll_hyde.csv";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter05\jekyll_hyde_table5.pdf";

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

            Table table = new Table(new float[] { 3, 32 });
            table.SetWidthPercent(100);

            List<List<string>> resultSet = CsvTo2DList.Convert(SRC, "|");
            resultSet.RemoveAt(0);

            table.AddHeaderCell("imdb")
                .AddHeaderCell("Information about the movie");

            foreach (List<string> record in resultSet)
            {
                table.AddCell(record[0]);
                Cell cell = new Cell()
                    .Add(new Paragraph(record[1]))
                    .Add(new Paragraph(record[2]))
                    .Add(new Paragraph(record[3]))
                    .Add(new Paragraph(record[4]))
                    .Add(new Paragraph(record[5]));
                cell.SetKeepTogether(true);
                table.AddCell(cell);
            }

            document.Add(table);

            document.Close();
        }
    }
}
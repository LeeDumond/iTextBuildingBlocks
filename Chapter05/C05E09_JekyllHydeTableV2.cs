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
    public class C05E09_JekyllHydeTableV2
    {
        private static readonly string SRC = $@"{Paths.DataResourcesPath}\jekyll_hyde.csv";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter05\jekyll_hyde_table2.pdf";

        public static void Main(string[] args)
        {
            FileInfo file = new FileInfo(DEST);
            if (file.Directory != null && !file.Directory.Exists)
            {
                file.Directory.Create();
            }

            CreatePdf(DEST);
        }

        private static void CreatePdf(string dest)
        {
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));

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
                foreach (string field in record)
                {
                    table.AddCell(field);
                }
            }

            Table outerTable = new Table(1)
                .AddHeaderCell("Continued from previous page:")
                .SetSkipFirstHeader(true)
                .AddCell(new Cell().Add(table).SetPadding(0));

            document.Add(outerTable);

            document.Close();
        }
    }
}
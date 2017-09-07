using System;
using System.Collections.Generic;
using System.IO;
using iText.IO.Image;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter05
{
    public class C05E10_JekyllHydeTableV3
    {
        private static readonly string SRC = $@"{Paths.DataResourcesPath}\jekyll_hyde.csv";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter05\jekyll_hyde_table3.pdf";

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
                Cell cell = new Cell();
                FileInfo file = new FileInfo($@"{Paths.ImageResourcesPath}\{record[0]}.jpg");

                if (file.Exists)
                {
                    Image img = new Image(ImageDataFactory.Create(file.FullName));
                    img.SetAutoScaleWidth(true);
                    cell.Add(img);                    
                }
                else
                {
                    cell.Add(record[0]);
                }
                table.AddCell(cell);

                table.AddCell(record[1]);
                table.AddCell(record[2]);
                table.AddCell(record[3]);
                table.AddCell(record[4]);
                table.AddCell(record[5]);
            }

            document.Add(table);

            document.Close();
        }
    }
}
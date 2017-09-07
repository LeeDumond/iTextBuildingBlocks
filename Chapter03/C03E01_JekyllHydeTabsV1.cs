using System;
using System.Collections.Generic;
using System.IO;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter03
{
    public class C03E01_JekyllHydeTabsV1
    {
        private static readonly string SRC = $@"{Paths.DataResourcesPath}\jekyll_hyde.csv";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter03\jekyll_hyde_tabs1.pdf";

        public static void Main(string[] args)
        {
            var file = new FileInfo(DEST);
            if (!file.Directory.Exists)
            {
                file.Directory.Create();
            }

            CreatePdf(DEST);
        }

        private static void CreatePdf(string dest)
        {
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            Document document = new Document(pdf, PageSize.A4.Rotate());

            PdfCanvas pdfCanvas = new PdfCanvas(pdf.AddNewPage());
            for (int i = 1; i <= 10; i++)
            {
                pdfCanvas.MoveTo(document.GetLeftMargin() + i * 50, 0);
                pdfCanvas.LineTo(document.GetLeftMargin() + i * 50, 595);
            }
            pdfCanvas.Stroke();

            List<List<string>> resultSet = CsvTo2DList.Convert(SRC, "|");

            foreach (List<string> record in resultSet)
            {
                Paragraph p = new Paragraph();

                p.Add(record[0].Trim()).Add(new Tab())
                    .Add(record[1].Trim()).Add(new Tab())
                    .Add(record[2].Trim()).Add(new Tab())
                    .Add(record[3].Trim()).Add(new Tab())
                    .Add(record[4].Trim()).Add(new Tab())
                    .Add(record[5].Trim());

                document.Add(p);
            }

            document.Close();
        }
    }
}
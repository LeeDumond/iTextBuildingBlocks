using System;
using System.Collections.Generic;
using System.IO;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter06
{
    public class C06E12_Outlines
    {
        private static readonly string SRC = $@"{Paths.DataResourcesPath}\jekyll_hyde.csv";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter06\jekyll_hyde_outlines.pdf";

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
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            pdf.AddNewPage();
            pdf.GetCatalog().SetPageMode(PdfName.UseOutlines);

            PdfOutline root = pdf.GetOutlines(false);

            List<List<string>> resultSet = CsvTo2DList.Convert(SRC, "|");
            resultSet.RemoveAt(0);

            foreach (List<string> record in resultSet)
            {
                PdfOutline movie = root.AddOutline(record[2]);

                PdfOutline imdb = movie.AddOutline("Link to IMDB");
                imdb.SetColor(Color.BLUE);
                imdb.SetStyle(PdfOutline.FLAG_BOLD);
                string url = $"http://www.imdb.com/title/tt{record[0]}";
                imdb.AddAction(PdfAction.CreateURI(url));

                PdfOutline info = movie.AddOutline("More info:");
                info.SetOpen(false);
                info.SetStyle(PdfOutline.FLAG_ITALIC);

                PdfOutline director = info.AddOutline($"Directed by {record[3]}");
                director.SetColor(Color.RED);

                PdfOutline place = info.AddOutline($"Produced in {record[4]}");
                place.SetColor(Color.MAGENTA);

                PdfOutline year = info.AddOutline("Released in " + record[1]);
                year.SetColor(Color.DARK_GRAY);
            }

            pdf.Close();
        }
    }
}
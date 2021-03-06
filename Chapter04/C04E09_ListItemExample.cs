﻿using System.Collections.Generic;
using System.IO;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iTextBuildingBlocks;

namespace Chapter04
{
    public class C04E09_ListItemExample
    {
        private static readonly string SRC = $@"{Paths.DataResourcesPath}\jekyll_hyde.csv";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter04\jekyll_hyde_overviewV4.pdf";

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
            Document document = new Document(pdf);

            List<List<string>> resultSet = CsvTo2DList.Convert(SRC, "|");
            resultSet.RemoveAt(0);

            List list = new List(ListNumberingType.DECIMAL);

            foreach (List<string> record in resultSet)
            {
                ListItem li = new ListItem();
                li.SetKeepTogether(true);

                string url = $"http://www.imdb.com/title/tt{record[0]}";
                Link movie = new Link(record[2], PdfAction.CreateURI(url));

                li.Add(new Paragraph(movie.SetFontSize(14)))
                    .Add(new Paragraph($"Directed by {record[3]} ({record[4]}, {record[1]})"));

                FileInfo file = new FileInfo($@"{Paths.ImageResourcesPath}\{record[0]}.jpg");

                if (file.Exists)
                {
                    Image img = new Image(ImageDataFactory.Create(file.FullName));
                    img.ScaleToFit(10000, 120);
                    li.Add(img);
                }

                list.Add(li);
            }

            document.Add(list);

            document.Close();
        }
    }
}
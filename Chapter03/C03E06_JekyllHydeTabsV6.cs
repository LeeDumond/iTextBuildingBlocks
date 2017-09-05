﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Action;
using iText.Kernel.Pdf.Canvas;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iTextBuildingBlocks;

namespace Chapter03
{
    public class C03E06_JekyllHydeTabsV6
    {
        private static readonly string SRC = $@"{Paths.DataResourcesPath}\jekyll_hyde.csv";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter03\jekyll_hyde_tabs6.pdf";

        public static void Main(string[] args)
        {
            var file = new FileInfo(DEST);
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

            float[] stops = { 40, 580, 590, 720 };
            List<TabStop> tabstops = new List<TabStop>();
            tabstops.Add(new TabStop(stops[0], TabAlignment.LEFT));
            tabstops.Add(new TabStop(stops[1], TabAlignment.RIGHT));
            tabstops.Add(new TabStop(stops[2], TabAlignment.LEFT));

            TabStop anchor = new TabStop(stops[3], TabAlignment.ANCHOR);
            anchor.SetTabAnchor(' ');
            tabstops.Add(anchor);

            PdfCanvas pdfCanvas = new PdfCanvas(pdf.AddNewPage());
            for (int i = 0; i < stops.Length; i++)
            {
                pdfCanvas.MoveTo(document.GetLeftMargin() + stops[i], 0);
                pdfCanvas.LineTo(document.GetLeftMargin() + stops[i], 595);
            }
            pdfCanvas.Stroke();

            List<List<String>> resultSet = CsvTo2DList.Convert(SRC, "|");

            foreach (List<String> record in resultSet)
            {
                Paragraph p = new Paragraph();

                p.AddTabStops(tabstops);

                PdfAction uri = PdfAction.CreateURI($"http://www.imdb.com/title/tt{record[0]}");
                Link link = new Link(record[2].Trim(), uri);

                p.Add(record[1].Trim()).Add(new Tab())
                    .Add(link).Add(new Tab())
                    .Add(record[3].Trim()).Add(new Tab())
                    .Add(record[4].Trim()).Add(new Tab())
                    .Add(record[5].Trim() + " \'");
                document.Add(p);
            }

            document.Close();
        }
    }
}
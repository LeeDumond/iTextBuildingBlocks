﻿using System.IO;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iTextBuildingBlocks;

namespace Chapter03
{
    public class C03E15_MaryReillyV8
    {
        private static readonly string MARY = $@"{Paths.ImageResourcesPath}\0117002.jpg";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter03\mary_reilly_V8.pdf";

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

            Paragraph p = new Paragraph(
                "Mary Reilly is a maid in the household of Dr. Jekyll: ");
            document.Add(p);

            Image img = new Image(ImageDataFactory.Create(MARY));
            img.SetHorizontalAlignment(HorizontalAlignment.CENTER);
            img.SetWidthPercent(80);
            document.Add(img);            

            document.Close();
        }
    }
}
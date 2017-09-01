using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Hyphenation;
using iText.Layout.Properties;

namespace Chapter02
{
    public class C02E06_JekyllHydeV2
    {
        private const string SRC = @"C:\Projects2\iTextBuildingBlocks\resources\txt\jekyll_hyde.txt";
        private const string DEST = @"C:\Projects2\iTextBuildingBlocks\results\chapter02\jekyll_hyde_v2.pdf";

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
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));

            //Initialize document
            Document document = new Document(pdf);
            document.SetTextAlignment(TextAlignment.JUSTIFIED).SetHyphenation(new HyphenationConfig("en", "uk", 3, 3));

            StreamReader sr = File.OpenText(SRC);
            string line;
            while ((line = sr.ReadLine()) != null)
            {
                document.Add(new Paragraph(line));
            }

            //Close document
            document.Close();
        }
    }
}
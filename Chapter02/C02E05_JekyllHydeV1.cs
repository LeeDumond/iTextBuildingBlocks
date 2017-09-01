using System;
using System.IO;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace Chapter02
{
    public class C02E05_JekyllHydeV1
    {
        private const string DEST = @"C:\Projects2\iTextBuildingBlocks\results\chapter02\jekyll_hyde_v1.pdf";
        private const string SRC = @"C:\Projects2\iTextBuildingBlocks\resources\txt\jekyll_hyde.txt";

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
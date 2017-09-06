using System;
using System.IO;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Hyphenation;
using iText.Layout.Properties;
using iTextBuildingBlocks;

namespace Chapter02
{
    public class C02E13_JekyllHydeV9
    {
        private static readonly string SRC = $@"{Paths.TextResourcesPath}\jekyll_hyde.txt";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter02\jekyll_hyde_v9.pdf";

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

            // Initialize document
            Document document = new Document(pdf, PageSize.A4, false);
            PdfFont font = PdfFontFactory.CreateFont(FontConstants.TIMES_ROMAN);
            PdfFont bold = PdfFontFactory.CreateFont(FontConstants.HELVETICA_BOLD);
            document.SetTextAlignment(TextAlignment.JUSTIFIED)
                .SetHyphenation(new HyphenationConfig("en", "uk", 3, 3))
                .SetFont(font)
                .SetFontSize(11);

            StreamReader sr = File.OpenText(SRC);
            string line;
            Paragraph p;
            bool title = true;
            while ((line = sr.ReadLine()) != null)
            {
                p = new Paragraph(line);
                p.SetKeepTogether(true);
                if (title)
                {
                    p.SetFont(bold).SetFontSize(12);
                    title = false;
                }
                else
                {
                    p.SetFirstLineIndent(36);
                }
                if (string.IsNullOrWhiteSpace(line))
                {
                    p.SetMarginBottom(12);
                    title = true;
                }
                else
                {
                    p.SetMarginBottom(0);
                }
                document.Add(p);
            }

            int n = pdf.GetNumberOfPages();

            for (int page = 1; page <= n; page++)
            {
                Paragraph footer = new Paragraph($"Page {page} of {n}");
                document.ShowTextAligned(footer, 297.5f, 20, page, TextAlignment.CENTER, VerticalAlignment.MIDDLE, 0);
            }

            //Close document
            document.Close();
        }
    }
}
﻿using System.IO;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Hyphenation;
using iText.Layout.Properties;
using iTextBuildingBlocks;

namespace Chapter04
{
    public class C04E05_ParagraphAndDiv3
    {
        private static readonly string SRC = $@"{Paths.TextResourcesPath}\jekyll_hyde.txt";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter04\jekyll_hydeV3.pdf";

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
            Document document = new Document(pdf);
            PdfFont font = PdfFontFactory.CreateFont(FontConstants.TIMES_ROMAN);
            PdfFont bold = PdfFontFactory.CreateFont(FontConstants.HELVETICA_BOLD);
            document.SetTextAlignment(TextAlignment.JUSTIFIED)
                .SetHyphenation(new HyphenationConfig("en", "uk", 3, 3));

            StreamReader sr = File.OpenText(SRC);
            string line;
            Div div = new Div();

            while ((line = sr.ReadLine()) != null)
            {
                document.Add(
                    new Paragraph(line)
                        .SetFont(bold)
                        .SetFontSize(12)
                        .SetMarginBottom(0)
                        .SetKeepWithNext(true));

                div = new Div()
                    .SetFont(font)
                    .SetFontSize(11)
                    .SetMarginBottom(18);

                while ((line = sr.ReadLine()) != null)
                {
                    div.Add(
                        new Paragraph(line)
                            .SetMarginBottom(0)
                            .SetFirstLineIndent(36)
                            .SetMultipliedLeading(1.2f)
                    );

                    if (string.IsNullOrWhiteSpace(line))
                    {
                        document.Add(div);
                        break;
                    }
                }
            }

            document.Add(div);

            //Close document
            document.Close();
        }
    }
}
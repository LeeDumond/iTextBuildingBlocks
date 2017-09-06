using System;
using System.IO;
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
    public class C04E04_ParagraphAndDiv1
    {
        private static readonly string SRC = $@"{Paths.TextResourcesPath}\jekyll_hyde.txt";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter04\jekyll_hydeV1.pdf";

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
                Paragraph title = new Paragraph(line)
                    .SetFont(bold)
                    .SetFontSize(12)
                    .SetMarginBottom(0);

                div = new Div()
                    .Add(title)
                    .SetFont(font)
                    .SetFontSize(11)
                    .SetMarginBottom(18);

                while ((line = sr.ReadLine()) != null)
                {
                    div.Add(
                        new Paragraph(line)
                            .SetMarginBottom(0)
                            .SetFirstLineIndent(36)
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
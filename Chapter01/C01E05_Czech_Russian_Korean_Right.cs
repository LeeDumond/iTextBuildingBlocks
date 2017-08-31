using System.IO;
using iText.IO.Font;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace Chapter01
{
    public class C01E05_Czech_Russian_Korean_Right
    {
        private const string DEST =
            @"C:\Projects2\iTextBuildingBlocks\results\chapter01\czech_russian_korean.pdf";

        private const string FONT = @"C:\Projects2\iTextBuildingBlocks\resources\fonts\FreeSans.ttf";
        private const string HCRBATANG = @"C:\Projects2\iTextBuildingBlocks\resources\fonts\HANBatang.ttf";

        private const string CZECH = "Podivn\u00fd p\u0159\u00edpad Dr. Jekylla a pana Hyda";

        private const string RUSSIAN = "\u0421\u0442\u0440\u0430\u043d\u043d\u0430\u044f "
                                       + "\u0438\u0441\u0442\u043e\u0440\u0438\u044f "
                                       + "\u0434\u043e\u043a\u0442\u043e\u0440\u0430 "
                                       + "\u0414\u0436\u0435\u043a\u0438\u043b\u0430 \u0438 "
                                       + "\u043c\u0438\u0441\u0442\u0435\u0440\u0430 "
                                       + "\u0425\u0430\u0439\u0434\u0430";

        private const string KOREAN = "\ud558\uc774\ub4dc, \uc9c0\ud0ac, \ub098";

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
            // Initialize PDF document
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));

            // Initialize document
            Document document = new Document(pdf);

            // Add content
            PdfFont font1250 = PdfFontFactory.CreateFont(FONT, PdfEncodings.CP1250, true);

            document.Add(new Paragraph().SetFont(font1250)
                .Add(CZECH).Add(" by Robert Louis Stevenson"));

            PdfFont font1251 = PdfFontFactory.CreateFont(FONT, "Cp1251", true);
            document.Add(new Paragraph().SetFont(font1251)
                .Add(RUSSIAN).Add(" by Robert Louis Stevenson"));

            PdfFont fontUnicode = PdfFontFactory.CreateFont(HCRBATANG, PdfEncodings.IDENTITY_H, true);
            document.Add(new Paragraph().SetFont(fontUnicode)
                .Add(KOREAN).Add(" by Robert Louis Stevenson"));

            //Close document
            document.Close();
        }
    }
}
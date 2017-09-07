using System.IO;
using iText.IO.Font;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iTextBuildingBlocks;

namespace Chapter04
{
    public class C04E08_CustomListSymbols
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter04\custom_list_symbols.pdf";
        private static readonly string INFO = $@"{Paths.ImageResourcesPath}\test\info.png";

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
            PageSize pagesize = PageSize.A6.Rotate();
            Document document = new Document(pdf, pagesize);
            //Set column parameters
            float offSet = 36;
            float gutter = 23;
            float columnWidth = (pagesize.GetWidth() - offSet * 2) / 2 - gutter;
            float columnHeight = pagesize.GetHeight() - offSet * 2;

            //Define column areas
            Rectangle[] columns = {
                new Rectangle(offSet, offSet, columnWidth, columnHeight),
                new Rectangle(offSet + columnWidth + gutter, offSet, columnWidth, columnHeight)};
            document.SetRenderer(new ColumnDocumentRenderer(document, columns));

            List list = new List();
            list.SetListSymbol("\u2022");
            list.Add("Dr. Jekyll");
            list.Add("Mr. Hyde");
            document.Add(list);

            list = new List();
            PdfFont font = PdfFontFactory.CreateFont(FontConstants.ZAPFDINGBATS);
            list.SetListSymbol(new Text("*").SetFont(font).SetFontColor(Color.ORANGE));
            list.SetSymbolIndent(10);
            list.Add("Dr. Jekyll");
            list.Add("Mr. Hyde");
            document.Add(list);

            Image info = new Image(ImageDataFactory.Create(INFO));
            info.ScaleAbsolute(12, 12);
            list = new List().SetSymbolIndent(3);
            list.SetListSymbol(info);
            list.Add("Dr. Jekyll");
            list.Add("Mr. Hyde");
            document.Add(list);

            list = new List();
            list.SetListSymbol(ListNumberingType.ENGLISH_LOWER);
            list.SetPostSymbolText("- ");
            list.Add("Dr. Jekyll");
            list.Add("Mr. Hyde");
            document.Add(list);

            list = new List(ListNumberingType.DECIMAL);
            list.SetPreSymbolText("Part ");
            list.SetPostSymbolText(": ");
            list.Add("Dr. Jekyll");
            list.Add("Mr. Hyde");
            document.Add(list);

            list = new List(ListNumberingType.DECIMAL);
            list.SetItemStartIndex(5);
            list.Add("Dr. Jekyll");
            list.Add("Mr. Hyde");
            document.Add(list);

            list = new List(ListNumberingType.ROMAN_LOWER);
            list.SetListSymbolAlignment(ListSymbolAlignment.LEFT);
            for (int i = 0; i < 6; i++)
            {
                list.Add("Dr. Jekyll");
                list.Add("Mr. Hyde");
            }
            document.Add(list);
            
            //Close document
            document.Close();
        }
    }
}
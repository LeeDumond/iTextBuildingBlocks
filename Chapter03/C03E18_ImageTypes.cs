using System.IO;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter03
{
    public class C03E18_ImageTypes
    {
        private static readonly string TEST1 = $@"{Paths.ImageResourcesPath}\test\map.jp2";
        private static readonly string TEST2 = $@"{Paths.ImageResourcesPath}\test\butterfly.bmp";
        private static readonly string TEST3 = $@"{Paths.ImageResourcesPath}\test\hitchcock.png";
        private static readonly string TEST4 = $@"{Paths.ImageResourcesPath}\test\info.png";
        private static readonly string TEST5 = $@"{Paths.ImageResourcesPath}\test\hitchcock.gif";
        private static readonly string TEST6 = $@"{Paths.ImageResourcesPath}\test\amb.jb2";
        private static readonly string TEST7 = $@"{Paths.ImageResourcesPath}\test\marbles.tif";

        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter03\image_types.pdf";

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

            // raw
            byte[] data = new byte[256 * 3];

            for (int i = 0; i < 256; i++)
            {
                data[i * 3] = (byte)(255 - i);
                data[i * 3 + 1] = (byte)(255 - i);
                data[i * 3 + 2] = (byte)i;
            }

            ImageData raw = ImageDataFactory.Create(256, 1, 3, 8, data, null);
            Image img = new Image(raw);
            img.ScaleAbsolute(256, 10);
            document.Add(img);

            // JPEG2000
            Image img1 = new Image(ImageDataFactory.Create(TEST1));
            document.Add(img1);
            document.Add(new AreaBreak());

            // BMP
            Image img2 = new Image(ImageDataFactory.Create(TEST2));
            img2.SetMarginBottom(10);
            document.Add(img2);

            // PNG
            Image img3 = new Image(ImageDataFactory.Create(TEST3));
            img3.SetMarginBottom(10);
            document.Add(img3);
            
            // Transparent PNG
            Image img4 = new Image(ImageDataFactory.Create(TEST4));
            img4.SetBorderLeft(new SolidBorder(6));
            document.Add(img4);

            // GIF
            Image img5 = new Image(ImageDataFactory.Create(TEST5));
            img5.SetBackgroundColor(Color.LIGHT_GRAY);
            document.Add(img5);

            // AWT
            System.Drawing.Image awtImage = System.Drawing.Image.FromFile(TEST5);
            Image awt = new Image(ImageDataFactory.Create(awtImage, System.Drawing.Color.Yellow));
            awt.SetMarginTop(10);
            document.Add(awt);

            // JBIG2
            Image img6 = new Image(ImageDataFactory.Create(TEST6));
            document.Add(img6);

            // TIFF
            Image img7 = new Image(ImageDataFactory.Create(TEST7));
            document.Add(img7);

            document.Close();
        }
    }
}
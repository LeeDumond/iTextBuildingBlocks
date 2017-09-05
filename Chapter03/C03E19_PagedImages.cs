using System;
using System.Collections.Generic;
using System.IO;
using iText.IO.Image;
using iText.IO.Source;
using iText.IO.Util;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter03
{
    public class C03E19_PagedImages
    {
        private static readonly string TEST1 = $@"{Paths.ImageResourcesPath}\test\animated_fox_dog.gif";
        private static readonly string TEST2 = $@"{Paths.ImageResourcesPath}\test\amb.jb2";
        private static readonly string TEST3 = $@"{Paths.ImageResourcesPath}\test\marbles.tif";

        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter03\paged_images.pdf";

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
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest));
            Document document = new Document(pdf);

            Image img;

            // Animated GIF
            Uri url1 = new Uri(TEST1);
            IList<ImageData> list = ImageDataFactory.CreateGifFrames(url1);

            foreach (ImageData data in list)
            {
                img = new Image(data);
                document.Add(img);
            }

            // JBIG2
            Uri url2 =new Uri(TEST2);
            IRandomAccessSource ras2 = new RandomAccessSourceFactory().CreateSource(url2);
            RandomAccessFileOrArray raf2 = new RandomAccessFileOrArray(ras2);
            int pages2 = Jbig2ImageData.GetNumberOfPages(raf2);
            for (int i = 1; i <= pages2; i++)
            {
                img = new Image(ImageDataFactory.CreateJbig2(url2, i));
                document.Add(img);
            }


            // TIFF
            Uri url3 = new Uri(TEST3);
            IRandomAccessSource ras3 = new RandomAccessSourceFactory().CreateSource(url3);
            RandomAccessFileOrArray raf3 = new RandomAccessFileOrArray(ras3);
            int pages3 = TiffImageData.GetNumberOfPages(raf3);
            for (int i = 1; i <= pages3; i++)
            {
                img = new Image(
                    ImageDataFactory.CreateTiff(url3, true, i, true));
                document.Add(img);
            }

            document.Close();
        }
    }
}
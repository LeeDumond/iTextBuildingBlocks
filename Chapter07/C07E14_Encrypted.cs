using System.IO;
using System.Text;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iTextBuildingBlocks;

namespace Chapter07
{
    public class C07E14_Encrypted
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter07\encrypted.pdf";

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
            byte[] user = Encoding.ASCII.GetBytes("It's Hyde");
            byte[] owner = Encoding.ASCII.GetBytes("abcdefg");
            PdfDocument pdf = new PdfDocument(new PdfWriter(dest,
                new WriterProperties().SetStandardEncryption(user, owner,
                    EncryptionConstants.ALLOW_PRINTING | EncryptionConstants.ALLOW_ASSEMBLY,
                    EncryptionConstants.ENCRYPTION_AES_256)));
            Document document = new Document(pdf);
            document.Add(new Paragraph("Mr. Jekyll has a secret: he changes into Mr. Hyde."));
            document.Close();
        }
    }
}
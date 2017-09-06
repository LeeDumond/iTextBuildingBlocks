using System.IO;
using iTextBuildingBlocks;

namespace Chapter05
{
    public class C05E01_MyFirstTable
    {
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter05\my_first_table.pdf";

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
            throw new System.NotImplementedException();
        }
    }
}
using System.IO;
using iTextBuildingBlocks;

namespace Chapter05
{
    public class C05E10_JekyllHydeTableV3
    {
        private static readonly string SRC = $@"{Paths.DataResourcesPath}\jekyll_hyde.csv";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter05\jekyll_hyde_table3.pdf";

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
            throw new System.NotImplementedException();
        }
    }
}
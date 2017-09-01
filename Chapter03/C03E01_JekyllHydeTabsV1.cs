using System;
using System.IO;
using iTextBuildingBlocks;

namespace Chapter03
{
    public class C03E01_JekyllHydeTabsV1
    {
        private static readonly string SRC = $@"{Paths.DataResourcesPath}\jekyll_hyde.csv";
        private static readonly string DEST = $@"{Paths.ResultsPath}\chapter03\jekyll_hyde_tabs1.pdf";

        public static void Main(string[] args)
        {
            var file = new FileInfo(DEST);
            if (file.Directory != null && !file.Directory.Exists)
            {
                file.Directory.Create();
            }

            CreatePdf(DEST);
        }

        private static void CreatePdf(string dest)
        {
            throw new NotImplementedException();
        }
    }
}
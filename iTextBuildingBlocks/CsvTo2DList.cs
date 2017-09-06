using System.Collections.Generic;
using System.IO;
using iText.IO.Util;

namespace iTextBuildingBlocks
{
    public static class CsvTo2DList
    {
        public static List<List<string>> Convert(string pathToSource, string seperator)
        {
            var resultSet = new List<List<string>>();

            var sr = File.OpenText(pathToSource);
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                var tokenizer = new StringTokenizer(line, seperator);
                var record = new List<string>();

                while (tokenizer.HasMoreTokens())
                {
                    record.Add(tokenizer.NextToken());
                }

                resultSet.Add(record);
            }

            return resultSet;
        }
    }
}
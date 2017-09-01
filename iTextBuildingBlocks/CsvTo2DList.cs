using System;
using System.Collections.Generic;
using System.IO;
using iText.IO.Util;

namespace iTextBuildingBlocks
{
    public class CsvTo2DList
    {
        public static List<List<string>> Convert(string pathToSource, string seperator)
        {
            List<List<string>> resultSet = new List<List<string>>();

            StreamReader sr = File.OpenText(pathToSource);
            string line;

            while ((line = sr.ReadLine()) != null)
            {
                StringTokenizer tokenizer = new StringTokenizer(line, seperator);
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
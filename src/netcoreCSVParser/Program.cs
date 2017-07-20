namespace netcoreCsvParser
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    
    class Program
    {
        public static void Main(string[] args)
        {
            List<string> columnRow = new List<string>();
            List<List<string>> dataRows = new List<List<string>>();

            try 
            {
                if (args.Length == 1) 
                {
                    using (Stream fileStream = new FileStream(args[0], FileMode.Open, FileAccess.Read))
                    {
                        using (StreamReader fileReader = new StreamReader(fileStream))
                        {
                            string line;
                            while ((line = fileReader.ReadLine()) != null)
                            {
                                if (columnRow.Count == 0)
                                {
                                    columnRow = getRow(line);
                                    continue;
                                }
                                if (line.Trim() != "")
                                {
                                    dataRows.Add(getRow(line));
                                }
                            }
                        }
                    }
                    Console.WriteLine(generateJson(columnRow, dataRows));
                }
                else 
                {
                    throw new ArgumentException("Exactly one argument required.");
                }
            } 
            catch (Exception caught)
            {
                Console.WriteLine("Exception Type    : " + caught.GetType());
                Console.WriteLine("Exception Message : " + caught.Message);
                Console.WriteLine(caught);
                Environment.Exit(-1);
            }
        }

        private static string generateJson(List<string> columnRow, List<List<string>> dataRows)
        {
            string result = "{\n  [\n    ";
            for (int rowIndex = 0; rowIndex < dataRows.Count; rowIndex++)
            {
                result += "{\n      ";
                for (int itemIndex = 0; itemIndex < dataRows[rowIndex].Count; itemIndex++)
                {
                    result += string.Format("\"{0}\": \"{1}\"", columnRow[itemIndex], dataRows[rowIndex][itemIndex]);
                    if (itemIndex < dataRows[rowIndex].Count - 1)
                    {
                        result += ",\n      ";
                    }
                }

                if (rowIndex < dataRows.Count - 1)
                {
                    result += "\n    },\n    ";
                    continue;
                }
                result += "\n    }";
            }
            result += "\n  ]\n}";
            return result;
        }

        private static List<string> getRow(string line)
        {
            string[] lineArray = line.Split(',');
            List<string> result = new List<string>();
            for (int index = 0; index < lineArray.Length; index++)
            {
                result.Add(lineArray[index].Trim());
            }
            return result;
        }
    }
}

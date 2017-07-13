namespace netcore_csv_parser
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
                                if (columnRow == null)
                                {
                                    columnRow = getRow(line);
                                }
                                else
                                {
                                    dataRows.Add(getRow(line));
                                }
                            }
                        }
                    }
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
                Environment.Exit(-1);
            }
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

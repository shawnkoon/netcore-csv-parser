using System.Collections.Generic;
using System.IO;

namespace netcoreCsvParser
{
    internal class FileParser
    {
        private string fileName;
        private List<string> columnRow;
        private List<List<string>> dataRows;
        private FileIO fileIO;

        internal FileParser(string fileName, FileIO fileIO)
        {
            this.fileName = fileName;
            this.columnRow = new List<string>();
            this.dataRows = new List<List<string>>();
            this.fileIO = fileIO;
        }

        internal void ExecuteJob()
        {
            using (Stream fileStream = new FileStream(this.fileName, FileMode.Open, FileAccess.Read))
            using (StreamReader fileReader = new StreamReader(fileStream))
            {
                string line;
                while ((line = fileReader.ReadLine()) != null)
                {
                    if (this.columnRow.Count == 0)
                    {
                        this.columnRow = fileIO.GetRow(line);
                        continue;
                    }
                    if (line.Trim() != "")
                    {
                        this.dataRows.Add(fileIO.GetRow(line));
                    }
                }
            }
            fileIO.Print(this.GenerateJson());
        }

        internal string GenerateJson()
        {
            string result = "{\n  [\n    ";
            for (int rowIndex = 0; rowIndex < this.dataRows.Count; rowIndex++)
            {
                result += "{\n      ";
                for (int itemIndex = 0; itemIndex < this.dataRows[rowIndex].Count; itemIndex++)
                {
                    result += string.Format("\"{0}\": \"{1}\"", this.columnRow[itemIndex], this.dataRows[rowIndex][itemIndex]);
                    if (itemIndex < this.dataRows[rowIndex].Count - 1)
                    {
                        result += ",\n      ";
                    }
                }

                if (rowIndex < this.dataRows.Count - 1)
                {
                    result += "\n    },\n    ";
                    continue;
                }
                result += "\n    }";
            }
            result += "\n  ]\n}";
            return result;
        }
    }
}
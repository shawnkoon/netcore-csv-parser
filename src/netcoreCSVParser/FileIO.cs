using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace netcoreCsvParser
{
    internal class FileIO
    {
        internal void Print(string line)
        {
            Console.WriteLine(line);
        }

        internal string GetErrorOut(Exception caught)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Exception Type : {caught.GetType()}");
            stringBuilder.AppendLine($"Exception Message: {caught.Message}");
            stringBuilder.AppendLine($"{caught}");
            return stringBuilder.ToString();
        }

        internal List<string> GetRow(string line)
        {
            return line?.Split(',')
                    .Select(data => data.Trim())
                    .ToList();
        }
    }
}
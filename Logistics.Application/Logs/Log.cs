using System;
using System.IO;

namespace Logistics.Application.Logs
{
    public class Log
    {
        public static void GravaLog(string sFileName, string sText)
        {
            try
            {
                using StreamWriter outputFile = new StreamWriter(Path.Combine($"{Directory.GetCurrentDirectory()}/Logs/" + sFileName + ".txt"), true);
                outputFile.Write($"{sText} - {DateTime.Now}{Environment.NewLine}");
            }
            catch { }
        }
    }
}

using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.Write("Enter the path to the log file: ");
        string logFilePath = Console.ReadLine();
        Console.Write("Enter the path to save the report: ");
        string outputFilePath = Console.ReadLine();

        try
        {

            LogStatistics logStats = AnalyzeLogFile(logFilePath);


            WriteReport(outputFilePath, logStats);

            Console.WriteLine("The report was successfully generated and saved to the file: " + outputFilePath);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: File not found.");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Error: No access to the file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }

    static LogStatistics AnalyzeLogFile(string filePath)
    {
        LogStatistics logStats = new LogStatistics();

        using (StreamReader reader = new StreamReader(filePath))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();


                if (line.Contains("ERROR"))
                {
                    logStats.ErrorCount++;
                }
                else if (line.Contains("WARNING"))
                {
                    logStats.WarningCount++;
                }
                else if (line.Contains("INFO"))
                {
                    logStats.InfoCount++;
                }


            }
        }

        return logStats;
    }

    static void WriteReport(string filePath, LogStatistics logStats)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Total number of entries: " + (logStats.ErrorCount + logStats.WarningCount + logStats.InfoCount));
            writer.WriteLine("Number of errors: " + logStats.ErrorCount);
            writer.WriteLine("Number of warnings: " + logStats.WarningCount);
            writer.WriteLine("Number of informational messages: " + logStats.InfoCount);


            writer.Flush();
        }
    }
}

class LogStatistics
{
    public int ErrorCount { get; set; }
    public int WarningCount { get; set; }
    public int InfoCount { get; set; }

}

using System.Collections;

namespace WebsiteAnalytics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] output = File.ReadAllLines("secondDay.txt");
            List<string> dublicates = new List<string>();
            List<string> secondDayVisitors = new List<string>();

            for (int i = 0; i < output.Length; i++)
            {
                string[] fields = output[i].Split(',');
                if (ReadRecord(fields[0], "firstDay.txt", 0) != "Record not found")
                    dublicates.Add(fields[0]);
                else secondDayVisitors.Add(fields[0]);
            }

            if (dublicates.Count > 0)
            {
                Console.WriteLine("Users that visited some pages on both days:");
                for (int i = 0; i < dublicates.Count; i++)
                {
                    Console.WriteLine(dublicates[i]);
                }
            }
            else Console.WriteLine("Dublicates not found");

            if (secondDayVisitors.Count > 0)
            {
                Console.WriteLine("Users that on the second day visited the page that hadn’t been visited by this user on the first day");
                for (int i = 0; i < secondDayVisitors.Count; i++)
                {
                    Console.WriteLine(secondDayVisitors[i]);
                }
            }
            else Console.WriteLine("All users that visited the page on the second day also visited that page on the first day");
        }

        public static string ReadRecord(string searchTerm, string filePath, int numberOfSearchTerm)
        {
            string recordNotFound = "Record not round";
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(',');
                    if (RecordMatches(searchTerm, fields, numberOfSearchTerm))
                    {
                        return lines[i];
                    }
                }
                return recordNotFound;
            }
            catch (Exception ex)
            {
                return recordNotFound;
                throw new Exception("An error occurred while reading the file: " + ex.Message);
            }
        }

        private static bool RecordMatches(string searchTerm, string[] record, int numberOfSearchTerm)
        {
            if (record[numberOfSearchTerm].Equals(searchTerm)) return true;
            return false;
        }
    }
}

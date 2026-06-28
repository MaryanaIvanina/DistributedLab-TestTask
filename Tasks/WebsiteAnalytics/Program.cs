using System.Collections;

namespace WebsiteAnalytics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] output = ReadMultipleRecords("23", "records.txt", 2);

            for (int i = 0; i < output.Length; i++)
            {
                Console.WriteLine(output[i]);
            }
        }

        public static string[] ReadMultipleRecords(string searchTerm, string filePath, int numberOfSearchTerm)
        {
            ArrayList records = new ArrayList();
            string[] recordNotFound = { "Record not round" };

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(',');
                    if (RecordMatches(searchTerm, fields, numberOfSearchTerm))
                    {
                        records.Add(lines[i]);
                    }
                }

                if (records.Count == 0) return recordNotFound; 
                return (string[])records.ToArray(typeof(string));
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

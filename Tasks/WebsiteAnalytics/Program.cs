using System.Collections;

namespace WebsiteAnalytics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] secondDay = File.ReadAllLines("secondDay.txt");
            string[] firstDay = File.ReadAllLines("firstDay.txt");

            List<string> dublicates = new List<string>();
            List<string> secondDayVisitors = new List<string>();

            bool isThisSecondVisitor = false;

            for (int i = 0; i < secondDay.Length; i++)
            {
                string[] fields = secondDay[i].Split(',');

                for (int j = 0; j < firstDay.Length; j++)
                {
                    string[] firsDayFields = firstDay[j].Split(",");
                    if (RecordMatches(fields[0], firsDayFields, 0))
                    {
                        if (!dublicates.Contains(fields[0]))
                        {
                            dublicates.Add(fields[0]);
                            break;
                        }
                    }
                }
                if (!secondDayVisitors.Contains(fields[0]) && !dublicates.Contains(fields[0]))
                {
                    secondDayVisitors.Add(fields[0]);
                }
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

        private static bool RecordMatches(string searchTerm, string[] record, int numberOfSearchTerm)
        {
            if (record[numberOfSearchTerm].Equals(searchTerm)) return true;
            return false;
        }
    }
}

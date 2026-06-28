namespace WebsiteAnalytics
{
    internal class WebsiteAnalyzer
    {
        public void AnalyzeTheWebsite()
        {
            Dictionary<string, HashSet<string>> day1Visits = new Dictionary<string, HashSet<string>>();

            foreach (string line in File.ReadLines("firstDay.txt"))
            {
                string[] parts = line.Split(',');
                if (parts.Length >= 2)
                {
                    string userId = parts[0];
                    string productId = parts[1];

                    if (!day1Visits.ContainsKey(userId))
                        day1Visits[userId] = new HashSet<string>();

                    day1Visits[userId].Add(productId);
                }
            }

            HashSet<string> targetUsers = new HashSet<string>();

            foreach (string line in File.ReadLines("secondDay.txt"))
            {
                string[] parts = line.Split(',');
                if (parts.Length >= 2)
                {
                    string userId = parts[0];
                    string productId = parts[1];

                    if (day1Visits.ContainsKey(userId) && !day1Visits[userId].Contains(productId))
                        targetUsers.Add(userId);
                }
            }

            OutputResults(targetUsers, "Users meeting both criteria:");
        }

        private void OutputResults(HashSet<string> users, string title)
        {
            if (users.Count > 0)
            {
                Console.WriteLine(title);
                foreach (string user in users)
                    Console.WriteLine(user);
            }
            else
                Console.WriteLine("No users found matching the criteria.");
        }
    }
}

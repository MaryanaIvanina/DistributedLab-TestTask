namespace Maze
{
    internal class InputData
    {
        public int ReadInput(string message)
        {
            while (true)
            {
                Console.WriteLine(message);
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Please enter a number.");
                    continue;
                }

                if (int.TryParse(input, out int output))
                    return output;
                else
                {
                    Console.WriteLine("Error! Please enter only numbers.");
                    continue;
                }
            }
        }
    }
}

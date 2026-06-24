namespace Pinatas
{
    internal class Program
    {
        static void Main()
        {
            int result = 0;

            Console.WriteLine("Enter an array of pinatas separated by spaces:");
            string input = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(input))
            {
                string[] splitedPinatasString = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int[] pinatas = new int[splitedPinatasString.Length];

                for (int i = 0; i < splitedPinatasString.Length; i++)
                {
                    if (!int.TryParse(splitedPinatasString[i], out pinatas[i]))
                    {
                        Console.WriteLine($"Error! Please enter only numbers.");
                        return;
                    }
                }

                PinataSolver solver = new PinataSolver();
                result = solver.CalculateMaxAmount(pinatas);


                Console.WriteLine($"Max amount of candies: {result}");
            }
            else
                Console.WriteLine("An empty line was entered.");
        }
    }
}

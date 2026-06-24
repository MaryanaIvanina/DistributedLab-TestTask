namespace StudentsCapital
{
    internal class Program
    {
        static void Main()
        {
            InputData input = new InputData();

            int N = input.ReadInput("Enter a max number of laptops:");
            int C = input.ReadInput("Enter a start-up capital:");
            int K = input.ReadInput("Enter a number of laptops you have:");

            int[] prices = input.ReadArrayInput("Enter prices:", K);
            int[] gains = input.ReadArrayInput("Enter expected gains:", K);

            CapitalSolver solver = new CapitalSolver();
            int result = solver.CalculateCapital(N, C, K, prices, gains);

            Console.WriteLine($"Capital at the end of the summer: {result}");
        }
    }
}
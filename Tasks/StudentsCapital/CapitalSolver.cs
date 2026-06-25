namespace StudentsCapital
{
    internal class CapitalSolver
    {
        public int CalculateCapital(int limit, int startCapital, int numberOfLaptops, int[] prices, int[] gains)
        {
            int capital = startCapital;

            for (int i = 0; i < limit; i++)
            {
                int theHighestProfit = 0;
                int theHighestProfitIndex = -1;

                for (int j = 0; j < numberOfLaptops; j++)
                {
                    if (gains[j] > theHighestProfit && prices[j] <= capital)
                    {
                        theHighestProfit = gains[j];
                        theHighestProfitIndex = j;
                    }
                }

                if (theHighestProfitIndex == -1)
                {
                    break;
                }

                capital += theHighestProfit;
                gains[theHighestProfitIndex] = 0;
            }

            return capital;
        }
    }
}
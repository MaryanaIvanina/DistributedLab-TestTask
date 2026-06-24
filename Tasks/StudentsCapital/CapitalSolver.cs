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

                for (int k = 0; k < numberOfLaptops; k++)
                {
                    if (gains[k] > theHighestProfit && prices[k] <= capital)
                    {
                        theHighestProfit = gains[k];
                        theHighestProfitIndex = k;
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
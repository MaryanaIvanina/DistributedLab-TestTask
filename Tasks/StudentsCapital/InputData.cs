namespace StudentsCapital
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
                    DisplayError("Please enter a number.");
                    continue;
                }

                if (int.TryParse(input, out int output)) 
                    return output;
                else 
                    DisplayError("Error! Please enter only numbers.");
            }
        }

        public int[] ReadArrayInput(string message, int numberOfLaptops)
        {
            while (true)
            {
                Console.WriteLine(message);
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    DisplayError("Please enter the numbers.");
                    continue;
                }

                string[] splitInput = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (splitInput.Length != numberOfLaptops)
                {
                    DisplayError($"Please enter exactly {numberOfLaptops} numbers.");
                    continue;
                }

                int[] array = new int[splitInput.Length];
                bool hasError = false;

                for (int i = 0; i < splitInput.Length; i++)
                {
                    if (!int.TryParse(splitInput[i], out array[i]))
                    {
                        DisplayError("Error! Please enter only numbers.");
                        hasError = true;
                        break;
                    }
                }

                if (!hasError) 
                    return array;
            }
        }

        private void DisplayError(string message)
        {
            Console.Clear();
            Console.WriteLine(message);
        }
    }
}
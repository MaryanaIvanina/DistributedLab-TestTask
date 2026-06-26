namespace Maze
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine("Enter the width of the maze:");
            string widthInput = Console.ReadLine();

            Console.WriteLine("Enter the height of the maze:");
            string heightInput = Console.ReadLine();

            if (!(int.TryParse(widthInput, out int width)))
            {
                Console.WriteLine("Error! Please enter only numbers.");
                return;
            }

            if (!(int.TryParse(heightInput, out int height)))
            {
                Console.WriteLine("Error! Please enter only numbers.");
                return;
            }

            MazeGenerator generator = new MazeGenerator();
            string[,] maze = generator.GenerateMaze(height, width);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (maze[i, j] == "#")
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    else if (maze[i, j] == "E")
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (maze[i, j] == "R")
                        Console.ForegroundColor = ConsoleColor.Red;
                    else if (maze[i, j] == "W")
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else if (maze[i, j] == "X")
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if (maze[i, j] == "T")
                        Console.ForegroundColor = ConsoleColor.White;

                    Console.Write(maze[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.ResetColor();
        }
    }
}

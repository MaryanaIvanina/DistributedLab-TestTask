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

            if (!int.TryParse(widthInput, out int width))
            {
                Console.WriteLine("Error! Please enter only numbers.");
                return;
            }

            if (!int.TryParse(heightInput, out int height))
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
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    else if (maze[i, j] == "R")
                        Console.ForegroundColor = ConsoleColor.Black;
                    else if (maze[i, j] == "X")
                        Console.ForegroundColor = ConsoleColor.Green;
                    else if (maze[i, j] == "T")
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else if (maze[i, j] == "P")
                        Console.ForegroundColor = ConsoleColor.Magenta;

                        Console.Write(maze[i, j] + " ");
                }
                Console.WriteLine();
            }

            int playerHeight = 0;
            int playerWidth = 0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (maze[i, j] == "E")
                    {
                        playerHeight = i;
                        playerWidth = j;
                        break;
                    }
                }
            }

            bool isTreasureFound = false;
            List<int[]> traps = new List<int[]>();

            while (true)
            {
                Console.Clear();

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (i == playerHeight && j == playerWidth)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("O ");
                        }
                        else
                        {
                            if (maze[i, j] == "#") Console.ForegroundColor = ConsoleColor.DarkGray;
                            else if (maze[i, j] == "E") Console.ForegroundColor = ConsoleColor.Cyan;
                            else if (maze[i, j] == "R") Console.ForegroundColor = ConsoleColor.Black;
                            else if (maze[i, j] == "X") Console.ForegroundColor = ConsoleColor.Green;
                            else if (maze[i, j] == "T")
                            {
                                if (isTreasureFound) Console.ForegroundColor = ConsoleColor.DarkGray;
                                else Console.ForegroundColor = ConsoleColor.Yellow;
                            }
                            else if (maze[i, j] == "P")
                            {
                                if (traps.Exists(t => t[0] == i && t[1] == j))
                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                else
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                            }

                            Console.Write(maze[i, j] + " ");
                        }
                    }
                    Console.WriteLine();
                }
                Console.ResetColor();

                if (maze[playerHeight, playerWidth] == "X")
                {
                    Console.WriteLine("\nВітаю! Ви знайшли вихід!");
                    break;
                }
                else if (maze[playerHeight, playerWidth] == "T")
                {
                    if (!isTreasureFound)
                    {
                        Console.WriteLine("\nВітаю! Ви знайшли скарб!");
                        isTreasureFound = true;
                    }
                }
                else if (maze[playerHeight, playerWidth] == "P")
                {
                    if (!traps.Exists(t => t[0] == playerHeight && t[1] == playerWidth))
                        traps.Add([playerHeight, playerWidth]);
                }

                if (traps.Count >= 3)
                {
                    Console.WriteLine("\nВи потрапили в 3 пастки! Гру завершено.");
                    break;
                }

                Console.WriteLine($"\nВи потрапили в {traps.Count} пастки!");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                int nextHeight = playerHeight;
                int nextWidth = playerWidth;

                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow: nextHeight--; break;
                    case ConsoleKey.DownArrow: nextHeight++; break;
                    case ConsoleKey.LeftArrow: nextWidth--; break;
                    case ConsoleKey.RightArrow: nextWidth++; break;
                }

                if (maze[nextHeight, nextWidth] != "#" && maze[nextHeight, nextWidth] != "E")
                {
                    playerHeight = nextHeight;
                    playerWidth = nextWidth;
                }
            }

            Console.ResetColor();
        }
    }
}

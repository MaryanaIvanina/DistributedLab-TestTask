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

            Console.Clear();

            MazeGenerator generator = new MazeGenerator();
            string[,] maze = generator.GenerateMaze(height, width);

            int playerHeight = 0;
            int playerWidth = 0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    switch (maze[i, j])
                    {
                        case "#": Console.ForegroundColor = ConsoleColor.DarkGray; break;
                        case "R": Console.ForegroundColor = ConsoleColor.Black; break;
                        case "E":
                            playerHeight = i;
                            playerWidth = j;
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            break;
                        case "X": Console.ForegroundColor = ConsoleColor.Green; break;
                        case "P": Console.ForegroundColor = ConsoleColor.Magenta; break;
                        case "T": Console.ForegroundColor = ConsoleColor.Yellow; break;
                    }

                    Console.Write(maze[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.SetCursorPosition(playerWidth * 2, playerHeight);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("O");

            bool isTreasureFound = false;
            List<int[]> traps = new List<int[]>();

            while (true)
            {
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
                    Console.SetCursorPosition(playerWidth * 2, playerHeight);

                    switch (maze[playerHeight, playerWidth])
                    {
                        case "#": Console.ForegroundColor = ConsoleColor.DarkGray; break;
                        case "R": Console.ForegroundColor = ConsoleColor.Black; break;
                        case "E": Console.ForegroundColor = ConsoleColor.Cyan; break;
                        case "X": Console.ForegroundColor = ConsoleColor.Green; break;
                        case "P":
                            if (traps.Exists(t => t[0] == playerHeight && t[1] == playerWidth))
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                            else
                                Console.ForegroundColor = ConsoleColor.Magenta;
                            break;
                        case "T":
                            if (isTreasureFound) Console.ForegroundColor = ConsoleColor.DarkGray;
                            else Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                    }

                    Console.Write(maze[playerHeight, playerWidth]);
                    Console.ResetColor();
                    Console.SetCursorPosition(0, height + 1);

                    playerHeight = nextHeight;
                    playerWidth = nextWidth;


                    Console.WriteLine($"\nYou`ve fallen into {traps.Count} traps!");

                    switch (maze[playerHeight, playerWidth])
                    {
                        case "X":
                            if (isTreasureFound) Console.SetCursorPosition(0, height + 5);
                            Console.WriteLine("\nCongratulations! You found the exit!");
                            return;
                        case "P":
                            if (!traps.Exists(t => t[0] == playerHeight && t[1] == playerWidth))
                                traps.Add([playerHeight, playerWidth]);
                            break;
                        case "T":
                            if (!isTreasureFound)
                            {
                                Console.WriteLine("\nCongratulations! You found the treasure!");
                                isTreasureFound = true;
                            }
                            break;
                    }

                    if (traps.Count >= 3)
                    {
                        Console.WriteLine("\nYou`ve fallen into 3 traps! The game is over.");
                        break;
                    }

                    Console.SetCursorPosition(playerWidth * 2, playerHeight);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("O");

                    Console.ResetColor();
                }
            }
        }
    }
}

namespace Maze
{
    internal class GameEngine
    {
        public void StartGame()
        {
            InputData input = new InputData();
            OutputData output = new OutputData();

            int width = input.ReadInput("Enter the width of the maze:");
            int height = input.ReadInput("Enter the height of the maze:");

            int neededWidth = (width * 2) + 5;
            int neededHeight = height + 7;

            if (OperatingSystem.IsWindows())
            {
                Console.BufferWidth = Math.Max(Console.BufferWidth, neededWidth);
                Console.BufferHeight = Math.Max(Console.BufferHeight, neededHeight);
                Console.WindowWidth = Math.Min(neededWidth, Console.LargestWindowWidth);
                Console.WindowHeight = Math.Min(neededHeight, Console.LargestWindowHeight);
            }
            Console.Clear();

            MazeGenerator generator = new MazeGenerator();
            string[,] maze = generator.GenerateMaze(height, width);

            bool isTreasureFound = false;
            List<int[]> usedTraps = new List<int[]>();

            int playerHeight = 0;
            int playerWidth = 0;

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    output.SetCellValue(maze[i, j], maze[i, j] + " ", i, j, usedTraps, isTreasureFound);

                    if (maze[i, j] == "E") {
                        playerHeight = i;
                        playerWidth = j;
                    }
                }
            }

            output.SetCellValue("O", "O", playerHeight, playerWidth, usedTraps, isTreasureFound);

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

                if (nextHeight >= 0 && nextHeight < height && nextWidth >= 0 && nextWidth < width && maze[nextHeight, nextWidth] != "#" && maze[nextHeight, nextWidth] != "E")
                {
                    output.SetCellValue(maze[playerHeight, playerWidth], maze[playerHeight, playerWidth], playerHeight, playerWidth, usedTraps, isTreasureFound);

                    Console.ResetColor();
                    Console.SetCursorPosition(0, height + 1);
                    Console.Write($"Traps triggered: {usedTraps.Count} / 3      ");

                    playerHeight = nextHeight;
                    playerWidth = nextWidth;

                    switch (maze[playerHeight, playerWidth])
                    {
                        case "X":
                            if (isTreasureFound) Console.SetCursorPosition(0, height + 3);
                            Console.WriteLine("Congratulations! You found the exit!");
                            return;
                        case "P":
                            if (!usedTraps.Exists(t => t[0] == playerHeight && t[1] == playerWidth))
                                usedTraps.Add([playerHeight, playerWidth]);
                            break;
                        case "T":
                            if (!isTreasureFound)
                            {
                                Console.WriteLine("Congratulations! You found the treasure!");
                                isTreasureFound = true;
                            }
                            break;
                    }

                    if (usedTraps.Count >= 3)
                    {
                        Console.WriteLine("You`ve fallen into 3 traps! The game is over.");
                        break;
                    }

                    output.SetCellValue("O", "O", playerHeight, playerWidth, usedTraps, isTreasureFound);
                }
            }
        }
    }
}

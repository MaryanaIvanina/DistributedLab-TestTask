namespace Maze
{
    internal class OutputData
    {
        public void SetCellColor(string cellValue, int height, int width, List<int[]> usedTraps, bool isTreasureFound)
        {
            switch (cellValue)
            {
                case "#": Console.ForegroundColor = ConsoleColor.DarkGray; break;
                case "R": Console.ForegroundColor = ConsoleColor.Black; break;
                case "O": Console.ForegroundColor = ConsoleColor.Green; break;
                case "E": Console.ForegroundColor = ConsoleColor.Cyan; break;
                case "X": Console.ForegroundColor = ConsoleColor.Green; break;
                case "P":
                    if (usedTraps.Exists(t => t[0] == height && t[1] == width))
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                    else
                        Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case "T":
                    if (isTreasureFound) Console.ForegroundColor = ConsoleColor.DarkGray;
                    else Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
            }
        }

        public void SetCellValue(string currentValue, string valueToSet, int height, int width, List<int[]> usedTraps, bool isTreasureFound)
        {
            Console.SetCursorPosition(width * 2, height);
            SetCellColor(currentValue, height, width, usedTraps, isTreasureFound);
            Console.Write(valueToSet);
        }
    }
}

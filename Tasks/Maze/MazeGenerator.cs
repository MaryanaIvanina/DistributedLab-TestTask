namespace Maze
{
    internal class MazeGenerator
    {
        string[,] maze;
        int height;
        int width;
        int startHeight;
        int startWidth;
        public string[,] GenerateMaze(int matrixHeight, int matrixWidth)
        {
            height = matrixHeight;
            width = matrixWidth;
            maze = new string[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    SetCell(i, j, "#");
                }
            }

            int numberOfExteriorWalls = (2 * (height + width)) - 4;
            int[,] exteriorWall = new int[2, numberOfExteriorWalls];
            int exteriorWallIndex = 0;

            int heightIndex;
            int widthIndex;

            int side = height;
            int otherSide = width;

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int k = 1; k < otherSide - 1; k++)
                    {
                        if (i != 1 && j != 1)
                        {
                            heightIndex = 0;
                            widthIndex = k;
                        }
                        else if (i != 1 && j == 1)
                        {
                            heightIndex = side - 1;
                            widthIndex = k;
                        }
                        else if (i == 1 && j != 1)
                        {
                            heightIndex = k;
                            widthIndex = side - 1;
                        }
                        else if (i == 1 && j == 1)
                        {
                            heightIndex = k;
                            widthIndex = 0;
                        }
                        else
                        {
                            heightIndex = 0;
                            widthIndex = 0;
                        }

                            SetCell(heightIndex, widthIndex, "W");

                        exteriorWall[0, exteriorWallIndex] = heightIndex;
                        exteriorWall[1, exteriorWallIndex] = widthIndex;
                        exteriorWallIndex++;
                    }
                }

                side = width;
                otherSide = height;
            }

            int entrance = new Random().Next(0, numberOfExteriorWalls);
            SetCell(exteriorWall[0, entrance], exteriorWall[1, entrance], "E");

            SearchStartingPoint(exteriorWall[0, entrance], exteriorWall[1, entrance]);
            SetCell(startHeight, startWidth, "R");
            CreateRoad(startHeight, startWidth);

            bool isExitFound = false;
            while (!isExitFound)
            {
                int exit = new Random().Next(0, numberOfExteriorWalls);

                if (exit != entrance && IsRoadNearby(exteriorWall[0, exit], exteriorWall[1, exit]))
                {
                    SetCell(exteriorWall[0, exit], exteriorWall[1, exit], "X");
                    isExitFound = true;
                    break;
                }
                else continue;
            }

            return maze;
        }

        private void SearchStartingPoint(int entranceHeight, int entranceWidth)
        {
            bool isStartingPointFound = false;
            int loopCounter = 0;
            int step = -1;

            while (!isStartingPointFound)
            {
                int startHeightCandidate = entranceHeight;
                int startWidthCandidate = entranceWidth;
                loopCounter++;
                step = step * -1;

                if (loopCounter >= 3) startWidthCandidate += step;
                else startHeightCandidate += step;

                if (startWidthCandidate >= 0 && startWidthCandidate < width && startHeightCandidate >= 0 && startHeightCandidate < height)
                {
                    if (maze[startHeightCandidate, startWidthCandidate] == "#")
                    {
                        startHeight = startHeightCandidate;
                        startWidth = startWidthCandidate;
                        isStartingPointFound = true;
                        break;
                    }
                    else continue;
                }
                else continue;
            }
        }

        private bool IsRoadNearby(int exitHeight, int exitWidth)
        {
            int[] dHeight = { -1, 1, 0, 0 };
            int[] dWidth = { 0, 0, -1, 1 };

            List<int> directions = new List<int> { 0, 1, 2, 3 };
            Shuffle(directions);

            foreach (int dir in directions)
            {
                int nextRoomHeight = exitHeight + dHeight[dir];
                int nextRoomWidth = exitWidth + dWidth[dir];

                if (nextRoomHeight > 0 && nextRoomHeight < height - 1 &&
                    nextRoomWidth > 0 && nextRoomWidth < width - 1)
                {
                    if (maze[nextRoomHeight, nextRoomWidth] == "R")
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void CreateRoad(int currentHeight, int currentWidth)
        {
            int[] dHeight = { -2, 2, 0, 0 };
            int[] dWidth = { 0, 0, -2, 2 };

            List<int> directions = new List<int> { 0, 1, 2, 3 };
            Shuffle(directions);

            foreach (int dir in directions)
            {
                int nextRoomHeight = currentHeight + dHeight[dir];
                int nextRoomWidth = currentWidth + dWidth[dir];

                if (nextRoomHeight > 0 && nextRoomHeight < height - 1 &&
                    nextRoomWidth > 0 && nextRoomWidth < width - 1)
                {
                    if (maze[nextRoomHeight, nextRoomWidth] == "#" || maze[nextRoomHeight, nextRoomWidth] == "W")
                    {
                        int wallToBreakHeight = currentHeight + (dHeight[dir] / 2);
                        int wallToBreakWidth = currentWidth + (dWidth[dir] / 2);

                        SetCell(wallToBreakHeight, wallToBreakWidth, "R");

                        SetCell(nextRoomHeight, nextRoomWidth, "R");

                        CreateRoad(nextRoomHeight, nextRoomWidth);
                    }
                }
            }
        }

        private void Shuffle(List<int> list)
        {
            Random random = new Random();
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                int temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        private void SetCell(int heightIndex, int widthIndex, string value)
        {
            maze[heightIndex, widthIndex] = value;
        }
    }
}

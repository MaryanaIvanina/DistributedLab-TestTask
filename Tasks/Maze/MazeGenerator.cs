namespace Maze
{
    internal class MazeGenerator
    {
        string[,] maze;
        int height;
        int width;
        int startHeight;
        int startWidth;
        int numberOfRoads;
        int exitHeight;
        int exitWidth;
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
            numberOfRoads++;
            CreateRoad(startHeight, startWidth);

            bool isExitFound = false;
            while (!isExitFound)
            {
                int exit = new Random().Next(0, numberOfExteriorWalls);

                if (exit != entrance && IsRoadNearby(exteriorWall[0, exit], exteriorWall[1, exit]))
                {
                    exitHeight = exteriorWall[0, exit];
                    exitWidth = exteriorWall[1, exit];
                    isExitFound = true;
                    break;
                }
                else continue;
            }

            SetCell(exitHeight, exitWidth, "X");

            int treasure = new Random().Next(0, numberOfRoads);
            if (treasure != 0)
            {
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        if (maze[i, j] == "R")
                        {
                            if (treasure == 0)
                            {
                                SetCell(i, j, "T");
                                break;
                            }
                            else treasure--;
                        }
                    }
                    if (treasure == 0) break;
                }
            }

            List<int[]> rightWay = FindTheRightWay(startHeight, startWidth, exitHeight, exitWidth);

            int numberOfTraps = new Random().Next(0, 5);

            if (numberOfTraps != 0)
            {
                int[,] traps = new int[numberOfTraps, 2];

                for (int i = 0; i < numberOfTraps; i++)
                {
                    bool isTrapPlaced = false;
                    while (!isTrapPlaced)
                    {
                        int trapHeight = new Random().Next(1, height - 1);
                        int trapWidth = new Random().Next(1, width - 1);

                        if (maze[trapHeight, trapWidth] == "R")
                        {
                            traps[i, 0] = trapHeight;
                            traps[i, 1] = trapWidth;
                            isTrapPlaced = true;
                            break;
                        }
                        else continue;
                    }
                }

                int trapsLimitOnTheRightWay = 2;

                for (int i = 0; i < numberOfTraps; i++)
                {
                    int trapHeight = traps[i, 0];
                    int trapWidth = traps[i, 1];
                    if (rightWay.Exists(cell => cell[0] == trapHeight && cell[1] == trapWidth))
                    {
                        if (trapsLimitOnTheRightWay != 0)
                        {
                            SetCell(trapHeight, trapWidth, "P");
                            trapsLimitOnTheRightWay--;
                        }
                    }
                    else
                        SetCell(trapHeight, trapWidth, "P");
                }
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

                        numberOfRoads += 2;

                        CreateRoad(nextRoomHeight, nextRoomWidth);
                    }
                }
            }
        }

        private List<int[]> FindTheRightWay(int startH, int startW, int exitH, int exitW)
        {
            Queue<int[]> queue = new Queue<int[]>();
            bool[,] visited = new bool[height, width];

            Dictionary<string, int[]> parentMap = new Dictionary<string, int[]>();

            queue.Enqueue(new int[] { startH, startW });
            visited[startH, startW] = true;

            int[] dH = { -1, 1, 0, 0 };
            int[] dW = { 0, 0, -1, 1 };

            while (queue.Count > 0)
            {
                int[] current = queue.Dequeue();
                int currH = current[0];
                int currW = current[1];

                if (currH == exitH && currW == exitW) break;

                for (int i = 0; i < 4; i++)
                {
                    int nextH = currH + dH[i];
                    int nextW = currW + dW[i];

                    if (nextH >= 0 && nextH < height && nextW >= 0 && nextW < width)
                    {
                        if (!visited[nextH, nextW] && (maze[nextH, nextW] == "R" || maze[nextH, nextW] == "E"))
                        {
                            visited[nextH, nextW] = true;
                            queue.Enqueue(new int[] { nextH, nextW });

                            parentMap[$"{nextH},{nextW}"] = new int[] { currH, currW };
                        }
                    }
                }
            }

            List<int[]> rightWay = new List<int[]>();
            string currentKey = $"{exitH},{exitW}";

            while (parentMap.ContainsKey(currentKey))
            {
                int[] parent = parentMap[currentKey];
                rightWay.Add(parent);
                currentKey = $"{parent[0]},{parent[1]}";
            }

            return rightWay;
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

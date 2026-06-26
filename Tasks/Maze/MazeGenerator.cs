namespace Maze
{
    internal class MazeGenerator
    {
        string[,] maze;
        int height;
        int width;
        int numberOfRoads;
        int exitHeight;
        int exitWidth;
        Random random = new Random();
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
            
            List<int[]> exteriorWalls = new List<int[]>();

            for (int j = 0; j < width; j++)
            {
                exteriorWalls.Add([0, j]);
                exteriorWalls.Add([height - 1, j]);
            }

            for (int i = 1; i < height - 1; i++)
            {
                exteriorWalls.Add([i, 0]);
                exteriorWalls.Add([i, width - 1]);
            }

            int numberOfExteriorWalls = exteriorWalls.Count;

            int entrance = random.Next(0, numberOfExteriorWalls);
            SetCell(exteriorWalls[entrance][0], exteriorWalls[entrance][1], "E");

            TryCheckAdjacentCells(1, exteriorWalls[entrance][0], exteriorWalls[entrance][1], 1, height - 1, 1, width - 1, false, "#", out int startHeight, out int startWidth);
            SetCell(startHeight, startWidth, "R");
            numberOfRoads++;
            CreateRoad(startHeight, startWidth);

            bool isExitFound = false;
            while (!isExitFound)
            {
                int exit = random.Next(0, numberOfExteriorWalls);
                if (TryCheckAdjacentCells(1, exteriorWalls[exit][0], exteriorWalls[exit][1], 1, height - 1, 1, width - 1, false, "R", out int roadHeight, out int roadtWidth))
                {
                    if (exit != entrance)
                    {
                        exitHeight = exteriorWalls[exit][0];
                        exitWidth = exteriorWalls[exit][1];
                        isExitFound = true;
                        break;
                    }
                }
            }

            SetCell(exitHeight, exitWidth, "X");

            if (random.Next(0, 2) == 1)
            {
                while (true)
                {
                    int trHeight = random.Next(1, height - 1);
                    int trWidth = random.Next(1, width - 1);

                    if (maze[trHeight, trWidth] == "R")
                    {
                        SetCell(trHeight, trWidth, "T");
                        break;
                    }
                }
            }

            List<int[]> rightWay = FindTheRightWay(startHeight, startWidth, exitHeight, exitWidth);


            int numberOfTraps = random.Next(0, 6);
            if (numberOfTraps != 0)
            {
                int trapsPlaced = 0;
                int trapsLimitOnTheRightWay = 2;

                while (trapsPlaced < numberOfTraps)
                {
                    int tHeight = random.Next(1, height - 1);
                    int tWidth = random.Next(1, width - 1);

                    if (maze[tHeight, tWidth] == "R")
                    {
                        bool isOnRightWay = rightWay.Exists(cell => cell[0] == tHeight && cell[1] == tWidth);

                        if (isOnRightWay && trapsLimitOnTheRightWay > 0)
                        {
                            SetCell(tHeight, tWidth, "P");
                            trapsLimitOnTheRightWay--;
                            trapsPlaced++;
                        }
                        else if (!isOnRightWay)
                        {
                            SetCell(tHeight, tWidth, "P");
                            trapsPlaced++;
                        }
                    }
                }
            }

            return maze;
        }

        private bool TryCheckAdjacentCells(int step, int currentHeight, int currentWidth, int minH, int maxH, int minW, int maxW, bool isCreatingRoad, string target, out int targetHeight, out int targetWidth)
        {
            SetStep(step, out int[] dHeight, out int[] dWidth);

            List<int> directions = new List<int> { 0, 1, 2, 3 };
            if (isCreatingRoad) Shuffle(directions);

            foreach (int dir in directions)
            {
                MakeStep(currentHeight, currentWidth, dHeight[dir], dWidth[dir], out targetHeight, out targetWidth);

                if (CellExist(targetHeight, targetWidth, minH, maxH, minW, maxW))
                {
                    if (maze[targetHeight, targetWidth] == target)
                    {
                        if (!isCreatingRoad) return true;
                        else
                        {
                            MakeStep(currentHeight, currentWidth, dHeight[dir] / 2, dWidth[dir] / 2, out int wallToBreakHeight, out int wallToBreakWidth);
                            SetCell(wallToBreakHeight, wallToBreakWidth, "R");
                            SetCell(targetHeight, targetWidth, "R");
                            numberOfRoads += 2;
                            CreateRoad(targetHeight, targetWidth);
                        }
                    }
                }
            }
            targetHeight = 0;
            targetWidth = 0;
            return false;
        }

        private void CreateRoad(int currentHeight, int currentWidth)
        {
            TryCheckAdjacentCells(2, currentHeight, currentWidth, 1, height - 1, 1, width - 1, true, "#", out int nextRoomHeight, out int nextRoomWidth);
        }

        private List<int[]> FindTheRightWay(int startH, int startW, int exitH, int exitW)
        {
            Queue<int[]> queue = new Queue<int[]>();
            bool[,] visited = new bool[height, width];

            Dictionary<string, int[]> parentMap = new Dictionary<string, int[]>();

            queue.Enqueue(new int[] { startH, startW });
            visited[startH, startW] = true;

            SetStep(1, out int[] dH, out int[] dW);

            while (queue.Count > 0)
            {
                int[] current = queue.Dequeue();
                int currH = current[0];
                int currW = current[1];

                if (currH == exitH && currW == exitW) break;

                for (int i = 0; i < 4; i++)
                {
                    MakeStep(currH, currW, dH[i], dW[i],
                        out int nextH, out int nextW);

                    if (CellExist(nextH, nextW, 0, height, 0, width))
                    {
                        if (!visited[nextH, nextW] && (maze[nextH, nextW] == "R" || maze[nextH, nextW] == "E" || maze[nextH, nextW] == "X"))
                        {
                            visited[nextH, nextW] = true;
                            queue.Enqueue([nextH, nextW]);

                            parentMap[$"{nextH},{nextW}"] = [currH, currW];
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
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                int temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        private void SetStep(int step, out int[] stepHeight, out int[] stepWidth)
        {
            stepHeight = [step * -1, step, 0, 0];
            stepWidth = [0, 0, step * -1, step];
        }

        private void MakeStep(int currentHeight, int currentWidth, int stepHeight, int stepWidth, out int nextHeight, out int nextWidth)
        {
            nextHeight = currentHeight + stepHeight;
            nextWidth = currentWidth + stepWidth;
        }

        private void SetCell(int heightIndex, int widthIndex, string value)
        {
            maze[heightIndex, widthIndex] = value;
        }

        private bool CellExist(int heightIndex, int widthIndex, int minHeight, int maxHeight, int minWidth, int maxWidth)
        {
            return heightIndex >= minHeight && heightIndex < maxHeight && widthIndex >= minWidth && widthIndex < maxWidth;
        }

    }
}

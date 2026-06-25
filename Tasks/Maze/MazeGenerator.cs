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

        private void CreateRoad(int currentHeight, int currentWidth)
        {
            List<List<int>> validSteps = CheckAdjacentCells(currentHeight, currentWidth);
            if (validSteps.Count != 0)
            {
                int randomIndex = new Random().Next(0, validSteps.Count);
                SetCell(validSteps[randomIndex][0], validSteps[randomIndex][1], "R");
                CreateRoad(validSteps[randomIndex][0], validSteps[randomIndex][1]);
            }
            else
                return;
        }

        private List<List<int>> CheckAdjacentCells(int currentHeight, int currentWidth)
        {
            List<List<int>> validSteps = new List<List<int>>();
            int step = -1;

            for (int j = 0; j < 4; j++)
            {
                List<int> indexes = new List<int>();
                int heightCandidate = currentHeight;
                int widthCandidate = currentWidth;
                step = step * -1;

                if (j >= 2) widthCandidate += step;
                else heightCandidate += step;

                if (heightCandidate >= 0 && heightCandidate < height && widthCandidate >= 0 && widthCandidate < width)
                {
                    if (maze[heightCandidate, widthCandidate] == "#")
                    {
                        indexes.Add(heightCandidate);
                        indexes.Add(widthCandidate);
                        validSteps.Add(indexes);
                    }
                }
            }

            return validSteps;
        }

        private void SetCell(int heightIndex, int widthIndex, string value)
        {
            maze[heightIndex, widthIndex] = value;
        }
    }
}

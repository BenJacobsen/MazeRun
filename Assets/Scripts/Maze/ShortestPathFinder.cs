using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//algo build on premise that first path to reach point is shortest
//each path diverges into as many paths are open
//if no more unexplored spaces are available then path is destroyed
namespace Assets.Scripts.Maze
{
    class ShortestPathFinder
    {
        public class ShortestPathFinderCellInfo
        {
            public ShortestPathFinderCellInfo()
            {
                Explored = false;
            }
            public bool Explored;
        }

        public static List<Cell> Find(MazeGrid maze, Cell startPoint, Cell endPoint)
        {

            Dictionary<Cell, ShortestPathFinderCellInfo> cellInfoDictionary = new Dictionary<Cell, ShortestPathFinderCellInfo>();
            maze.ListForm.ForEach((x) => cellInfoDictionary.Add(x, new ShortestPathFinderCellInfo()));
            List<List<Cell>> paths = new List<List<Cell>> { new List<Cell>() { startPoint } };

            while (paths.Count != 0)
            {
                List<List<Cell>> newPaths = new List<List<Cell>>();
                foreach (List<Cell> path in paths)
                {
                    List<Cell> openCells = maze.getConnectedCells(path.Last()).Where((x) => !cellInfoDictionary[x].Explored).ToList();
                    foreach (Cell openCell in openCells)
                    {
                        cellInfoDictionary[openCell].Explored = true;
                        List<Cell> newPath = new List<Cell>(path);
                        newPath.Add(openCell);
                        if (openCell == endPoint)
                        {
                            return newPath;
                        }
                        newPaths.Add(newPath);
                    }
                }
                paths = newPaths;
            }
            throw new InvalidProgramException("The points that were used as arguments are not connected in the maze, thus there is no path.");
        }

        //remove intermediate cells between cells that have a clear path between them
        public static List<Cell> CreateTrimmedPath(MazeGrid maze, List<Cell> path)
        {
            List<Cell> trimmedPath = new List<Cell>(path);
            Cell startCell = trimmedPath[0];
            while(startCell != trimmedPath.Last())
            {
                int startCellIndex = trimmedPath.FindIndex((cell) => cell == startCell);
                int endCellIndex = startCellIndex + 1;

                while(endCellIndex < trimmedPath.Count && HasClearPathBetween(maze, startCell, trimmedPath[endCellIndex]))
                {
                    endCellIndex++;
                }
                if (endCellIndex - startCellIndex > 2)
                {
                    trimmedPath.RemoveRange(startCellIndex + 1, (endCellIndex - startCellIndex) - 2);
                }
                startCell = trimmedPath[startCellIndex + 1];
            }
            return trimmedPath;
        }
        //if there were a rectangle with the two points on opposing corners, return true if there are no walls inside rectangle
        public static bool HasClearPathBetween(MazeGrid maze, Cell firstPoint, Cell secondPoint)
        {
            int xlower = Math.Min(firstPoint.X, secondPoint.X);
            int xupper = Math.Max(firstPoint.X, secondPoint.X);
            int ylower = Math.Min(firstPoint.Y, secondPoint.Y);
            int yupper = Math.Max(firstPoint.Y, secondPoint.Y);
            for (int xpos = xlower; xpos <= xupper; xpos++)
            {
                for (int ypos = ylower; ypos <= yupper; ypos++)
                {
                    Cell cellToCheck = maze.GridForm[xpos, ypos];
                    if ((cellToCheck.W && xpos != xlower) || (cellToCheck.E && xpos != xupper) || (cellToCheck.S && ypos != ylower)
                        || (cellToCheck.N && ypos != yupper))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
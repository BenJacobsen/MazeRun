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
    }
}
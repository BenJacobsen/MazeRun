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
        public static List<Cell> Find (MazeGrid maze, Cell startPoint, Cell endPoint)
        {

            maze.ListForm
            bool endFound = false;
            List<List<Cell>> paths = new List<List<Cell>> { new List<Cell>() { startPoint } };
            List<List<Cell>> newPaths;

            while (endFound == false)
            {
                foreach (List<Cell> path in paths)
                {
                    foreach (Cell openCell in maze.PathFind(path.Last()))
                    {

                    }
                }
                paths.ForEach((x) => {

                    endFound = (x.Last === endPoint);

                });
                newPaths.ForEach()
            } 
        }
}

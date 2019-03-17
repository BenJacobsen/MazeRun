using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMaze{
    public static System.Random rnd;

    public static MazeGrid Apply (int dim, int sparsity) {
        rnd = new System.Random();
        MazeGrid maze = new MazeGrid(dim);

        foreach (Cell cell in maze.ListForm)
        {
            maze.SetWall(SparsityLookup(sparsity), cell, Direction.north);
            maze.SetWall(SparsityLookup(sparsity), cell, Direction.east);
        }

        List<List<Cell>> Grouped =  CellGroup(maze);
        Grouped.Sort((a, b) => b.Count - a.Count);
        int index = 0;
        foreach(List<Cell> listCell in Grouped)
        {
            foreach (Cell cell in listCell)
            {
                cell.GroupIndex = index;
            }
            index++;
        }

        ConnectCells(maze, Grouped);

        return maze;
    }

    public static List<List<Cell>> CellGroup (MazeGrid maze)
    {
        List<List<Cell>> grouped = new List<List<Cell>>();

        List<Cell> ungrouped = new List<Cell>(maze.ListForm);

        while (ungrouped.Count != 0)
        {
            List<Cell> connected = Explore(maze, ungrouped[rnd.Next(ungrouped.Count)]);

            foreach (Cell cell in connected)
            {
                ungrouped.Remove(cell);
            }
            grouped.Add(connected);
        }
        
        return grouped;
    }
	
    public static List<Cell> Explore (MazeGrid maze, Cell startcell)
    {
        List<Cell> Connected = new List<Cell>();
        maze.Current = startcell;
        maze.Current.Explored = true;
        Connected.Add(maze.Current);

        while (maze.Current.Key != startcell.Key || maze.PathFind().Count != 0)
        {

            if(maze.PathFind().Count != 0)
            {
                maze.CurrAdvance(maze.PathFind()[0]);
                Connected.Add(maze.Current);
            }
            else
            {
                maze.CurrReverse();
            }
        }
        
        return Connected;
    }

    public static void ConnectCells (MazeGrid maze, List<List<Cell>> Grouped)
    {
        while (Grouped.Count > 1)
        {
            int connectedGroupIndex = -1;
            foreach (Cell cell in Grouped[Grouped.Count-1])
            {
                List<Direction> shuffledWalls = Shuffle(cell.WallCheck());
                foreach (Direction wallDir in shuffledWalls)    //wallcheck for oob
                {
                    if (!Grouped[Grouped.Count-1].Contains(maze.NextTo(cell, wallDir)))
                    {
                        maze.SetWall(false, cell, wallDir);
                        connectedGroupIndex = maze.NextTo(cell, wallDir).GroupIndex;
                        break;
                    }
                }
                if (connectedGroupIndex != -1)
                {
                    break;
                }
            }
            foreach(Cell cell in Grouped[Grouped.Count - 1])
            {
                cell.GroupIndex = connectedGroupIndex;
                Grouped[connectedGroupIndex].Add(cell);
            }
            Grouped.Remove(Grouped[Grouped.Count-1]);
        }
    }

    public static List<Direction> Shuffle (List<Direction> dirList)
    {
        List<Direction> shuffled = new List<Direction>();
        Direction dir;
        while(dirList.Count > 0)
        {
            dir = dirList[rnd.Next(dirList.Count)];
            shuffled.Add(dir);
            dirList.Remove(dir);
        }
        return shuffled;
    }

    private static bool SparsityLookup (int sparse)
    {
        if (sparse == 1)
        {
            return rnd.Next(3) > 1;
        }
        if (sparse == 2)
        {
            return rnd.Next(5) > 2;
        }
        if (sparse == 3)
        {
            return rnd.Next(2) > 0;
        }
        if (sparse == 4)
        {
            return rnd.Next(5) > 1;
        }
        else
        {
            return false;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomMazeCellInfo {
    public RandomMazeCellInfo ()
    {
        Explored = false;
    }
    public bool Explored;
    public Cell BackCell;
}

public class RandomMaze {
    private static System.Random m_rnd;
    private static Dictionary<Cell, RandomMazeCellInfo> m_cellInfo;

    public static MazeGrid Apply (int dim, int sparsity) {

        m_rnd = new System.Random();

        MazeGrid maze = new MazeGrid(dim);
        m_cellInfo = new Dictionary<Cell, RandomMazeCellInfo>();
        maze.ListForm.ForEach((x) => m_cellInfo.Add(x, new RandomMazeCellInfo()));
        foreach (Cell cell in maze.ListForm)
        {
            maze.SetWall(SparsityLookup(sparsity), cell, Direction.north);
            maze.SetWall(SparsityLookup(sparsity), cell, Direction.east);
        }

        //group connected cells of maze
        List<List<Cell>> grouped = new List<List<Cell>>();
        List<Cell> ungrouped = new List<Cell>(maze.ListForm);

        while (ungrouped.Count != 0)
        {
            List<Cell> connected = Explore(maze, ungrouped[m_rnd.Next(ungrouped.Count)]);

            foreach (Cell cell in connected)
            {
                ungrouped.Remove(cell);
            }
            grouped.Add(connected);
        }
        grouped.Sort((a, b) => b.Count - a.Count);

        //connect divided cells
        while (grouped.Count > 1)
        {
            foreach (Cell cell in grouped[grouped.Count - 1])
            {
                bool cellFound = false;
                List<Direction> shuffledWalls = Shuffle(cell.WallCheck());
                foreach (Direction wallDir in shuffledWalls)
                {
                    Cell adjacentCell = maze.NextTo(cell, wallDir);
                    if (!grouped[grouped.Count - 1].Contains(adjacentCell))
                    {
                        maze.SetWall(false, cell, wallDir);
                        grouped.Find((x) => x.Contains(adjacentCell)).AddRange(grouped[grouped.Count - 1]);
                        grouped.Remove(grouped[grouped.Count - 1]);
                        cellFound = true;
                        break;
                    }
                }
                if (cellFound)
                {
                    break;
                }
            }
        }

        return maze;
    }
	
    public static List<Cell> Explore (MazeGrid maze, Cell startcell)
    {
        List<Cell> connected = new List<Cell>();
        Cell current = startcell;
        
        connected.Add(current);
        while (current != startcell || maze.getConnectedCells(startcell).Where((x) => !m_cellInfo[x].Explored).Any())
        {
            List<Cell> unexploredCells = maze.getConnectedCells(current).Where((x) => !m_cellInfo[x].Explored).ToList();
            if (unexploredCells.Any())
            {
                Cell temp = current;
                current = unexploredCells[0];
                m_cellInfo[current].Explored = true;
                m_cellInfo[current].BackCell = temp;
                connected.Add(current);
            }
            else
            {
                current = m_cellInfo[current].BackCell;
            }
        }
        
        return connected;
    }

    public static List<Direction> Shuffle(List<Direction> dirList)
    {
        List<Direction> shuffled = new List<Direction>();
        Direction dir;
        while (dirList.Count > 0)
        {
            dir = dirList[m_rnd.Next(dirList.Count)];
            shuffled.Add(dir);
            dirList.Remove(dir);
        }
        return shuffled;
    }

    private static bool SparsityLookup (int sparse)
    {
        if (sparse == 1)
        {
            return m_rnd.Next(3) > 1;
        }
        if (sparse == 2)
        {
            return m_rnd.Next(5) > 2;
        }
        if (sparse == 3)
        {
            return m_rnd.Next(2) > 0;
        }
        if (sparse == 4)
        {
            return m_rnd.Next(5) > 1;
        }
        else
        {
            return false;
        }
    }

}

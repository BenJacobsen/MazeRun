/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlgCell : Cell
{
    public bool Explored;
    public Direction BackDir;

    public AlgCell(Cell cell)
    {
        Explored = false;
        N = cell.N;
        S = cell.S;
        E = cell.E;
        W = cell.W;
        X = cell.X;
        Y = cell.Y;
        Key = cell.Key;
    }

    public AlgCell()
    {
        X = -1;
        Y = -1;
    }

    public AlgCell(int coorx, int coory, int key)
    {
        Explored = false;
        N = false;
        S = false;
        E = false;
        W = false;
        X = coorx;
        Y = coory;
        Key = key;
    }

}

public class AlgGrid : MazeGrid
{
    public new AlgCell[,] GridForm;
    public new List<AlgCell> ListForm;
    public AlgCell Current;

    public AlgGrid(MazeGrid maze)
    {
        Dimension = maze.Dimension;
        GridForm = new AlgCell[Dimension, Dimension];
        ListForm = new List<AlgCell>(Dimension * Dimension);
        Current = new AlgCell();
        for (int y = 0; y < Dimension; y++)
        {
            for (int x = 0; x < Dimension; x++)
            {
                AlgCell algCell = new AlgCell(maze.GridForm[x, y]);
                GridForm[x, y] = algCell;

                ListForm.Add(algCell);
            }
        }
    }

    public List<Direction> PathFind()
    {
        List<Direction> OpenPaths = Current.NoWalls();
        List<Direction> Paths = new List<Direction>();
        if (Current.X == -1 || Current.Y == -1)
        {
            return null;
        }
        foreach (Direction direction in OpenPaths)
        {
            if (!NextTo(Current, direction).Explored)
            {
                Paths.Add(direction);
            }
        }
        return Paths;
    }

    public void CurrAdvance(Direction direction)
    {
        Current = NextTo(Current, direction);
        Current.Explored = true;
        Current.BackDir = DirectionInfo.Reverse(direction);
    }

    public void CurrReverse ()
    {
        Current = NextTo(Current, Current.BackDir);
    }

    public AlgCell NextTo(AlgCell algCell, Direction direction)
    {
        if (direction == Direction.north)
        {
            return GridForm[algCell.X, algCell.Y + 1];
        }

        if (direction == Direction.south)
        {
            return GridForm[algCell.X, algCell.Y - 1];
        }

        if (direction == Direction.east)
        {
            return GridForm[algCell.X + 1, algCell.Y];
        }

        return GridForm[algCell.X - 1, algCell.Y];
    }
} */

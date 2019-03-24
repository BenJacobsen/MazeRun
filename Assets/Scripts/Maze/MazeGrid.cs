using System.Collections;
using System.Collections.Generic;
using System.Text;
public class MazeGrid {

    public Cell[,] GridForm;
    public List<Cell> ListForm;
    public int Dimension;
    private static System.Random m_rnd;

    public MazeGrid ()
    {

    }
    public MazeGrid(int dim)
    {
        m_rnd = new System.Random();
        Dimension = dim;
        GridForm = new Cell[dim, dim];
        ListForm = new List<Cell>(dim * dim);
        for (int y = 0; y < dim; y++)
        {
            for (int x = 0; x < dim; x++)
            {
                Cell cell = new Cell(x, y);
                cell.ApplyWallsFromList(BoundCheck(cell));
                GridForm[x, y] = cell;

                ListForm.Add(cell);
            }
        }
    }

    public void SetWall(bool set, Cell cell, Direction direction)
    {
        foreach(Direction boundWall in BoundCheck(cell))
        {
            if (boundWall == direction)
            {
                return;
            }
        }

        if (direction == Direction.north)
        {
            cell.N = set;
            GridForm[cell.X, cell.Y + 1].S = set;
        }

        if (direction == Direction.south)
        {
            cell.S = set;
            GridForm[cell.X, cell.Y - 1].N = set;
        }

        if (direction == Direction.east)
        {
            cell.E = set;
            GridForm[cell.X + 1, cell.Y].W = set;
        }

        if (direction == Direction.west)
        {
            cell.W = set;
            GridForm[cell.X - 1, cell.Y].E = set;
        }
    }

    public Cell NextTo(Cell cell, Direction direction)
    {
        if (direction == Direction.north && cell.Y < Dimension-1)
        {
            return GridForm[cell.X, cell.Y + 1];
        }

        if (direction == Direction.south && cell.Y > 0)
        {
            return GridForm[cell.X, cell.Y - 1];
        }

        if (direction == Direction.east && cell.X < Dimension-1)
        {
            return GridForm[cell.X + 1, cell.Y];
        }

        if (direction == Direction.west && cell.X > 0)
        {
            return GridForm[cell.X - 1, cell.Y];
        }

        return cell;
    }

    public List<Direction> BoundCheck(Cell cell)
    {
        List<Direction> WallList = new List<Direction>();
        if (cell.X == 0)
        {
            WallList.Add(Direction.west);
        }

        if (cell.X == Dimension-1)
        {
            WallList.Add(Direction.east);
        }

        if (cell.Y == 0)
        {
            WallList.Add(Direction.south);
        }

        if (cell.Y == Dimension-1)
        {
            WallList.Add(Direction.north);
        }

        return WallList;
    }

    public List<Cell> getConnectedCells(Cell cellToFindFor)
    {
        //cell check
        List<Direction> noWallDirections = cellToFindFor.NoWalls();
        List<Cell> Paths = new List<Cell>();
        if (cellToFindFor.X == -1 || cellToFindFor.Y == -1)
        {
            return null;
        }
        foreach (Direction direction in noWallDirections)
        {
            Paths.Add(NextTo(cellToFindFor, direction));
        }
        return Paths;
    }

    public Cell getRandomCell ()
    {
        return ListForm[m_rnd.Next(ListForm.Count)];
    }
}


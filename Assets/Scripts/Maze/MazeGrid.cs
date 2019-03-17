using System.Collections;
using System.Collections.Generic;
using System.Text;
public class MazeGrid {

    public Cell[,] GridForm;
    public List<Cell> ListForm;
    public int Dimension;
    public Cell Current;

    public MazeGrid ()
    {

    }
    public MazeGrid(int dim)
    {
        Dimension = dim;
        GridForm = new Cell[dim, dim];
        ListForm = new List<Cell>(dim * dim);
        int keycount = 0;
        Current = new Cell();
        for (int y = 0; y < dim; y++)
        {
            for (int x = 0; x < dim; x++)
            {
                Cell cell = new Cell(x, y, keycount);
                cell.ApplyWallsFromList(BoundCheck(cell));
                GridForm[x, y] = cell;

                ListForm.Add(cell);
                keycount++;
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

    public List<Direction> PathFind(Cell cellToFindFor)
    {
        List<Direction> OpenPaths = Current.NoWalls();
        List<Direction> Paths = new List<Direction>();
        if (cellToFindFor.X == -1 || cellToFindFor.Y == -1)
        {
            return null;
        }
        foreach (Direction direction in OpenPaths)
        {
            if (!NextTo(cellToFindFor, direction).Explored)
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

    public void CurrReverse()
    {
        Current = NextTo(Current, Current.BackDir);
    }

    public void ClearExplored ()
    {
        ListForm.ForEach(x => x.Explored = false);
    }

}


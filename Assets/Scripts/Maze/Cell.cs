using System.Collections;
using System.Collections.Generic;

public class Cell
{
    public bool N;
    public bool S;
    public bool E;
    public bool W;
    public int X;
    public int Y;
    public Cell()
    {

    }

    public Cell(int coorx, int coory)
    {
        N = false;
        S = false;
        E = false;
        W = false;
        X = coorx;
        Y = coory;
    }

    public List<Direction> WallCheck()
    {
        List<Direction> WallList = new List<Direction>();

        if (W)
        {
            WallList.Add(Direction.west);
        }

        if (E)
        {
            WallList.Add(Direction.east);
        }

        if (S)
        {
            WallList.Add(Direction.south);
        }

        if (N)
        {
            WallList.Add(Direction.north);
        }

        return WallList;
    }

    public List<Direction> NoWalls()
    {
        List<Direction> wallList = WallCheck();
        List<Direction> noWallList = new List<Direction>();
        foreach (Direction dir in DirectionInfo.AllDirections)
        {
            if (!wallList.Contains(dir))
            {
                noWallList.Add(dir);
            }
        }
        return noWallList;
    }

    public void ApplyWallsFromList (List<Direction> dirs)
    {
        foreach (Direction dir in dirs)
        {
            if (dir == Direction.north)
            {
                N = true;
            }

            if (dir == Direction.south)
            {
                S = true;
            }

            if (dir == Direction.east)
            {
                E = true;
            }

            if (dir == Direction.west)
            {
                W = true;
            }
        }
    }
}

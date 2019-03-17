using System.Collections.Generic;
public enum Direction { north, south, east, west }

public static class DirectionInfo
{
    public static Direction[] DirectionArray = { Direction.north, Direction.south, Direction.east, Direction.west };

    public static List<Direction> AllDirections = new List<Direction>(DirectionArray);

    public static Direction Reverse(Direction direction)
    {
        if (direction == Direction.north)
        {
            return Direction.south;
        }

        if (direction == Direction.south)
        {
            return Direction.north;
        }

        if (direction == Direction.east)
        {
            return Direction.west;
        }

        return Direction.east;
    }
}



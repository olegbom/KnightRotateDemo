using System.Numerics;

namespace KnightRotateDemo;

public enum Direction: byte
{
    One = 0,
    Two = 1,
    Four = 2,
    Five = 3,
    Seven = 4,
    Eight = 5,
    Ten = 6,
    Eleven = 7
}

public static class DirectionHelper
{
    public static int DeltaX(this Direction dir) {
        return dir switch
        {
            Direction.One => 1,
            Direction.Two => 2,
            Direction.Four => 2,
            Direction.Five => 1,
            Direction.Seven => -1,
            Direction.Eight => -2,
            Direction.Ten => -2,
            Direction.Eleven => -1,
            _ => throw new System.NotImplementedException(),
        };
    }

    public static int DeltaY(this Direction dir)
    {
        return dir switch
        {
            Direction.One => -2,
            Direction.Two => -1,
            Direction.Four => 1,
            Direction.Five => 2,
            Direction.Seven => 2,
            Direction.Eight => 1,
            Direction.Ten => -1,
            Direction.Eleven => -2,
            _ => throw new System.NotImplementedException(),
        };
    }

    public static Direction RotateClockwise(this Direction dir)
    {
        return (Direction)(((int)dir + 1) & 0x7);
    }

    public static Direction RotateAnticlockwise(this Direction dir)
    {
        return (Direction)(((int)dir + 7) & 0x7);
    }

    public static Vector2 RotateClockwise(this Direction dir, float t)
    {
        return Vector2.One;
    }
}

using Raylib_cs;

namespace KnightRotateDemo;

public class Manipulator
{
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Direction { get; set;} = Direction.One;

    public Manipulator()
    {

    }

    public void Draw(int cellSize)
    {
        Raylib.DrawCircle(
            (int)(cellSize * (X + 0.5) + 1),
            (int)(cellSize * (Y + 0.5) + 1),
            (int)(cellSize * 0.4), Color.Brown);
        Raylib.DrawCircle(
            (int)(cellSize * (X + Direction.DeltaX() + 0.5) + 1),
            (int)(cellSize * (Y + Direction.DeltaY() + 0.5) + 1),
            (int)(cellSize * 0.4), Color.DarkBlue);
    }
}

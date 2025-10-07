using System.Numerics;
using Raylib_cs;

namespace KnightRotateDemo;

public class Manipulator
{
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Direction { get; set;} = Direction.One;
    // public bool Is
    public Manipulator()
    {

    }

    public void InputProcessing()
    {
        if( Raylib.IsKeyPressed(KeyboardKey.Q) )
        {
            Direction = Direction.RotateAnticlockwise();
        }

        if( Raylib.IsKeyPressed(KeyboardKey.E))
        {
            Direction = Direction.RotateClockwise();
        }
    }

    public void Draw(int cellSize)
    {
        Raylib.DrawCircleV(
            new Vector2(
                cellSize * (X + 0.5f) + 1,
                cellSize * (Y + 0.5f) + 1
            ),
            cellSize * 0.4f, Color.Brown);
        Raylib.DrawCircleLinesV(
            new Vector2(
                cellSize * (X + Direction.DeltaX() + 0.5f) + 1,
                cellSize * (Y + Direction.DeltaY() + 0.5f) + 1),
            (int)(cellSize * 0.4), Color.DarkBlue);
    }
}

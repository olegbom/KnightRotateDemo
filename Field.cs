using System.Numerics;
using Raylib_cs;

namespace KnightRotateDemo;

public class Field
{
    public int Size { get; init; } = 8;
    public int CellSize { get; init; } = 40;
    public Manipulator Manipulator;

    public Field()
    {
        Manipulator = new Manipulator(){X = 4, Y = 4};
    }

    public void GridDraw()
    {
        Raylib.DrawRectangleLines(1, 0, CellSize*Size, CellSize*Size, Color.Red);
        for(int i = 0; i < Size; i++)
        {
            for(int j = 0; j < Size; j++)
            {
                if (((i + j) % 2) == 1)
                {
                    Raylib.DrawRectangle(CellSize * i, CellSize * j, CellSize, CellSize, new Color(0, 0, 0, 100));
                }
            }
        }

        // Vector2 a = new Vector2(-1.0f / 8, 1.0f / 4) * CellSize;
        // Vector2 b = new Vector2(1.0f / 8, 3.0f / 4) * CellSize;
        // Vector2 c = new Vector2(1.0f / 8, 1.0f / 4) * CellSize;
        // Vector2 d = new Vector2(-1.0f / 8, 3.0f / 4) * CellSize;
        // for (int i = 0; i < Size - 1; i++)
        // {
        //     for (int j = 0; j < Size - 2; j++)
        //     {

        //         Vector2 s = new((i + 1) * CellSize, (j + 1) * CellSize);
        //         Raylib.DrawLineEx(s + a, s + b, 0.5f, Color.Blue);
        //         Raylib.DrawLineEx(s + c, s + d, 0.5f, Color.Blue);
        //         s = new((j + 1) * CellSize, (i + 1) * CellSize);
        //         Raylib.DrawLineEx(s + new Vector2(a.Y, a.X), s + new Vector2(b.Y, b.X), 0.5f, Color.Blue);
        //         Raylib.DrawLineEx(s + new Vector2(c.Y, c.X), s + new Vector2(d.Y, d.X), 0.5f, Color.Blue);
        //     }
        // }

        Manipulator.Draw(CellSize);
    }
}

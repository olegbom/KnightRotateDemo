using Raylib_cs;
using System;
using System.IO;
using System.Collections.Generic;

namespace KnightRotateDemo;

public class Field
{
    public int Size { get; init; } = 10;

    public int CellSize { get; init; } = 40;

    public Manipulator Manipulator;
    public Field()
    {
        Manipulator = new Manipulator(){X = 4, Y = 4};
    }

    public void GridDraw()
    {
        int max = CellSize * Size + 1;
        for (int i = 0; i < Size + 1; i++)
        {
            int t = CellSize * i + 1;
            Raylib.DrawLine(0, t, max, t, Color.Red);
            Raylib.DrawLine(t, 0, t, max, Color.Red);
        }

        Manipulator.Draw(CellSize);
    }
}

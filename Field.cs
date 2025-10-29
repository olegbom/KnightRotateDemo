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

        Manipulator.Draw(CellSize);
    }
}

using Raylib_cs;
using System;
using System.IO;
using System.Collections.Generic;

namespace KnightRotateDemo;

internal static class Program
{
    // STAThread is required if you deploy using NativeAOT on Windows - See https://github.com/raylib-cs/raylib-cs/issues/301
    [System.STAThread]
    public static void Main()
    {
        Raylib.InitWindow(800, 480, "Hello World");
        Field f = new Field();
        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            f.GridDraw();
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}

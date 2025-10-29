using Raylib_cs;
using System.Runtime.InteropServices.JavaScript;
using System.Numerics;

namespace KnightRotateDemo;

public partial class Application
{
    private static Field f = new();
    // STAThread is required if you deploy using NativeAOT on Windows - See https://github.com/raylib-cs/raylib-cs/issues/301
    // [System.STAThread]
    public static void Main()
    {
        Raylib.SetConfigFlags(ConfigFlags.Msaa4xHint);
        Raylib.InitWindow(800, 480, "Hello World");
        Raylib.SetTargetFPS(60);
    }

    [JSExport]
    public static void UpdateFrame()
    {
        Gesture currentGesture = Raylib.GetGestureDetected();
        Vector2 touchPosition = Raylib.GetTouchPosition(0);
        f.Manipulator.InputProcessing();

        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.White);

        f.GridDraw();
        if (currentGesture != Gesture.None) Raylib.DrawCircleV(touchPosition, 30, Color.Maroon);
        Raylib.EndDrawing();
    }
}

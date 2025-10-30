using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace KnightRotateDemo;

public class Manipulator
{
    public static double DiractionAnimationDuration { get;set; } = 0.3;

    private enum DirectionAnimationMode
    {
        None,
        Clockwise,
        Anticlockwise,
    }
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Direction { get; set;} = Direction.One;
    public bool IsGrabbed {get;set;} = false;
    private DirectionAnimationMode _animationMode = DirectionAnimationMode.None;
    private double _directionAnimationStartTime = 0;
    private readonly Queue<DirectionAnimationMode> _delayedAnimations = [];

    public Manipulator()
    {

    }

    public void InputProcessing()
    {
        if( Raylib.IsKeyPressed(KeyboardKey.A) )
        {
            if( _animationMode == DirectionAnimationMode.None )
            {
                Direction = Direction.RotateAnticlockwise();
                _animationMode = DirectionAnimationMode.Anticlockwise;
                _directionAnimationStartTime = Raylib.GetTime();
            }
            else
            {
                _delayedAnimations.Enqueue(DirectionAnimationMode.Anticlockwise);
            }
        }

        if( Raylib.IsKeyPressed(KeyboardKey.D))
        {
            if(_animationMode == DirectionAnimationMode.None )
            {
                Direction = Direction.RotateClockwise();
                _animationMode = DirectionAnimationMode.Clockwise;
                _directionAnimationStartTime = Raylib.GetTime();
            }
            else
            {
                _delayedAnimations.Enqueue(DirectionAnimationMode.Clockwise);
            }
        }

        if( Raylib.IsKeyPressed(KeyboardKey.W))
        {

        }

        if( Raylib.IsKeyPressed(KeyboardKey.R) )
        {
            IsGrabbed = !IsGrabbed;
        }
    }

    public void Draw(int cellSize)
    {
        float r = cellSize * (2.5f - System.MathF.Sqrt(5.0f));
        Vector2 c = new(
                cellSize * (X + 0.5f),
                cellSize * (Y + 0.5f)
            );
        Raylib.DrawCircleV(c, cellSize * (System.MathF.Sqrt(8.0f) - 5.0f/2), Color.Red);

        void DrawDirectionAnimation(DirectionAnimationMode mode)
        {
            double duration = Raylib.GetTime() - _directionAnimationStartTime;
            if (duration >= DiractionAnimationDuration)
            {
                if (_delayedAnimations.TryDequeue(out DirectionAnimationMode nextMode))
                {
                    _animationMode = nextMode;
                    switch (nextMode)
                    {
                        case DirectionAnimationMode.Anticlockwise:
                            Direction = Direction.RotateAnticlockwise();
                            break;
                        case DirectionAnimationMode.Clockwise:
                            Direction = Direction.RotateClockwise();
                            break;
                    }
                    _directionAnimationStartTime = Raylib.GetTime();
                    DrawDirectionAnimation(nextMode);
                }
                else
                {
                    _animationMode = DirectionAnimationMode.None;
                    DrawCircle(Direction.Delta());
                }
            }
            else
            {
                float t = (float)(duration / DiractionAnimationDuration);
                t = AnimationHelper.BizzareMoving(t);
                Vector2 delta = mode == DirectionAnimationMode.Anticlockwise
                                    ? Direction.RotateClockwise().RotateAnticlockwise(t)
                                    : Direction.RotateAnticlockwise().RotateClockwise(t);
                DrawCircle(delta);
            }
        }

        switch (_animationMode)
        {
            case DirectionAnimationMode.Anticlockwise:
                DrawDirectionAnimation(_animationMode);
                break;
            case DirectionAnimationMode.Clockwise:
                DrawDirectionAnimation(_animationMode);
                break;
            case DirectionAnimationMode.None:
                DrawCircle(Direction.Delta());
                break;
            default:
                break;
        }

        void DrawCircle(Vector2 delta)
        {
            Vector2 m = new(
                        cellSize * (X + delta.X + 0.5f),
                        cellSize * (Y + delta.Y + 0.5f)
                    );
            Raylib.DrawCircleLinesV( m, r, IsGrabbed ? Color.Blue : Color.DarkBlue);
            Raylib.DrawLineV(c + delta*cellSize*0.2f, c + delta*cellSize*0.8f, Color.Blue);
        }
    }
}

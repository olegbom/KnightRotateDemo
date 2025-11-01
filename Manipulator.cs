using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace KnightRotateDemo;

public class Manipulator(Field field)
{
    public static double DiractionAnimationDuration { get;set; } = 0.0;

    private enum DirectionAnimationMode
    {
        None,
        Clockwise,
        Anticlockwise,
    }

    public int X { get; set; }
    public int Y { get; set; }
    public Direction Direction { get; set;} = Direction.One;
    public bool IsGrabbed { get; set; } = false;
    public Field Field { get; } = field;
    private DirectionAnimationMode _animationMode = DirectionAnimationMode.None;
    private double _directionAnimationStartTime = 0;
    private readonly Queue<DirectionAnimationMode> _delayedAnimations = [];

    public void Swap()
    {
        if (_animationMode == DirectionAnimationMode.None)
        {
            int newX = X + Direction.DeltaX();
            int newY = Y + Direction.DeltaY();
            if (newX >= 0 && newX < 8 &&
                newY >= 0 && newY < 8)
            {
                X = newX;
                Y = newY;
                Direction = (Direction)(((int)Direction + 4) % 8);
            }
        }
    }

    public void TurnAround()
    {
        if (_animationMode == DirectionAnimationMode.None)
        {
            Direction = Direction.RotateClockwise();
            _animationMode = DirectionAnimationMode.Clockwise;
            _directionAnimationStartTime = Raylib.GetTime();
        }
        else
        {
            _delayedAnimations.Enqueue(DirectionAnimationMode.Clockwise);
        }

        for (int i = 0; i < 3; i++)
        {
            _delayedAnimations.Enqueue(DirectionAnimationMode.Clockwise);
        }
    }

    public void RotateClockwise()
    {
        if (_animationMode == DirectionAnimationMode.None)
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

    public void RotateAnticlockwise()
    {
        if (_animationMode == DirectionAnimationMode.None)
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
            double maxDuration = DiractionAnimationDuration / (1 + _delayedAnimations.Count);
            if (duration >= maxDuration)
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
                float t = (float)(duration / maxDuration);
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
            Vector2 m = cellSize * (new Vector2(X + 0.5f, Y + 0.5f) + delta);
            if ( IsGrabbed )
            {
                for(int i = 0; i < 4; i++)
                {
                    Raylib.DrawRingLines(m, r + 3, r + 3.5f, 90 * i + 15, 90 * (i + 1) - 15, 7, Color.Blue);
                }
            }
            else
            {
                Raylib.DrawCircleLinesV(m, r, Color.Blue);
            }
            Raylib.DrawLineV(c + delta*cellSize*0.2f, c + delta*cellSize*0.8f, Color.Blue);
        }
    }
}

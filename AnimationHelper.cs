namespace KnightRotateDemo;

public static class AnimationHelper
{
    public static float BizzareMoving(float t)
    {
        float t2 = t * t;
        return t * t2 * (6 * t2 - 15 * t + 10);
    }
}

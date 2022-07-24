public enum Direction
{
    None = -1,
    Up = 0,
    Down = 3,
    Left = 1,
    Right = 2
}

public static class DirectionMethods
{
    public static Direction Opposite(this Direction d)
    {
        return (Direction)(3 - (int)d);
    }
}
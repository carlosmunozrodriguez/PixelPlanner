namespace PixelPlanner.Entities;

public record GridCoordinates
{
    public int X { get; }

    public int Y { get; }

    private GridCoordinates(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Result<GridCoordinates> Create(int x, int y)
    {
        if (!ValidPosition(x))
        {
            return new FailedResult<GridCoordinates>(ErrorMessages.InvalidX);
        }

        if (!ValidPosition(y))
        {
            return new FailedResult<GridCoordinates>(ErrorMessages.InvalidY);
        }

        return Result<GridCoordinates>.Successful(new GridCoordinates(x, y));
    }

    private static bool ValidPosition(int p) => p >= 0;

    private static class ErrorMessages
    {
        public const string InvalidX = "Invalid X: X shouldn't be negative.";
        public const string InvalidY = "Invalid Y: Y shouldn't be negative.";
    }
}
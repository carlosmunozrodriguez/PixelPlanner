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

    public static Result Create(int x, int y)
    {
        if (!ValidPosition(x))
        {
            return Result.Failed(ErrorMessages.InvalidX);
        }

        if (!ValidPosition(y))
        {
            return Result.Failed(ErrorMessages.InvalidY);
        }

        return Result.Successful(new GridCoordinates(x, y));
    }

    private static bool ValidPosition(int p) => p >= 0;

    private static class ErrorMessages
    {
        public const string InvalidX = "Invalid X: X shouldn't be negative.";
        public const string InvalidY = "Invalid Y: Y shouldn't be negative.";
    }
}
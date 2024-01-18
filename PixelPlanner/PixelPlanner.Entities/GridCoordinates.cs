namespace PixelPlanner.Entities;

public record GridCoordinates
{
    public int X { get; }

    public int Y { get; }

    public GridCoordinates(int x, int y)
    {
        if (!ValidPosition(x))
        {
            throw new ArgumentOutOfRangeException(nameof(x), x, ErrorMessages.InvalidX);
        }

        if (!ValidPosition(y))
        {
            throw new ArgumentOutOfRangeException(nameof(y), y, ErrorMessages.InvalidY);
        }

        X = x;
        Y = y;
    }

    private static bool ValidPosition(int p) => p >= 0;

    private static class ErrorMessages
    {
        public const string InvalidX = "Invalid X: X shouldn't be negative.";
        public const string InvalidY = "Invalid Y: Y shouldn't be negative.";
    }
}
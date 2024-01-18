namespace PixelPlanner.Entities;

public record Rectangle
{
    public int Width { get; }

    public int Height { get; }

    public Rectangle(int width, int height)
    {
        if (!ValidLength(width))
        {
            throw new ArgumentOutOfRangeException(nameof(width), width, ErrorMessages.InvalidWidth);
        }

        if (!ValidLength(height))
        {
            throw new ArgumentOutOfRangeException(nameof(height), height, ErrorMessages.InvalidHeight);
        }

        Width = width;
        Height = height;
    }

    private static bool ValidLength(int x) => x > 0;


    private static class ErrorMessages
    {
        public const string InvalidWidth = "Invalid Width: Width should be a positive integer.";
        public const string InvalidHeight = "Invalid Height: Height should be a positive integer.";
    }
}
namespace PixelPlanner.Entities;

public record Rectangle
{
    public int Width { get; }

    public int Height { get; }

    private Rectangle(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public static Result<Rectangle> Create(int width, int height)
    {
        if (!ValidLength(width))
        {
            return Result<Rectangle>.Failed(ErrorMessages.InvalidWidth);
        }

        if (!ValidLength(height))
        {
            return Result<Rectangle>.Failed(ErrorMessages.InvalidHeight);
        }

        return Result<Rectangle>.Successful(new Rectangle(width, height));
    }

    private static bool ValidLength(int x) => x > 0;


    private static class ErrorMessages
    {
        public const string InvalidWidth = "Invalid Width: Width should be a positive integer.";
        public const string InvalidHeight = "Invalid Height: Height should be a positive integer.";
    }
}
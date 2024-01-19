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

    public static Result Create(int width, int height)
    {
        if (!ValidLength(width))
        {
            return Result.Failed(ErrorMessages.InvalidWidth);
        }

        if (!ValidLength(height))
        {
            return Result.Failed(ErrorMessages.InvalidHeight);
        }

        return Result.Successful(new Rectangle(width, height));
    }

    private static bool ValidLength(int x) => x > 0;


    private static class ErrorMessages
    {
        public const string InvalidWidth = "Invalid Width: Width should be a positive integer.";
        public const string InvalidHeight = "Invalid Height: Height should be a positive integer.";
    }
}
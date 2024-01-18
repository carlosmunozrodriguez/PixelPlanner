namespace PixelPlanner.Entities;

public class Grid
{
    private readonly List<PositionedRectangle> _rectanglesInGrid = [];

    public Guid Id { get; }

    public int Width { get; }

    public int Height { get; }

    public IReadOnlyCollection<PositionedRectangle> Rectangles => _rectanglesInGrid.AsReadOnly();

    private Grid(int width, int height)
    {
        Id = Guid.NewGuid();
        Width = width;
        Height = height;
    }

    public static Result<Grid> CreateGrid(int width, int height)
    {
        if (!IsValidLength(width))
        {
            return new FailedResult<Grid>(ErrorMessages.InvalidWidth);
        }

        if (!IsValidLength(height))
        {
            return new FailedResult<Grid>(ErrorMessages.InvalidHeight);
        }

        return new SuccessfulResult<Grid>(new Grid(width, height));
    }

    public Result<Guid> AddRectangle(Rectangle rectangle, GridCoordinates position)
    {
        var valid = ValidatePositionInsideGrid(position, rectangle);
        if (!valid)
        {
            return Result<Guid>.Failed(ErrorMessages.OutsideOfGrid);
        }

        var candidatePositionedRectangle = new PositionedRectangle(rectangle, position);

        valid = ValidateRectangleDoesNotOverlap(candidatePositionedRectangle);
        if (!valid)
        {
            return Result<Guid>.Failed(ErrorMessages.OverlappingRectangle);
        }

        _rectanglesInGrid.Add(candidatePositionedRectangle);
        return Result<Guid>.Successful(candidatePositionedRectangle.Id);
    }

    public Result<Void> RemoveRectangle(Guid rectangleId)
    {
        var rectangleIndex = _rectanglesInGrid.FindIndex(x => x.Id == rectangleId);

        if (rectangleIndex == -1)
        {
            return new FailedResult<Void>(ErrorMessages.NotFound(rectangleId));
        }

        _rectanglesInGrid.RemoveAt(rectangleIndex);
        return Result<Void>.Successful(Void.Nothing);
    }

    private static bool IsValidLength(int x) => x > 0;

    private bool ValidatePositionInsideGrid(GridCoordinates position, Rectangle rectangle) =>
        IsXInsideGrid(position.X) && IsXInsideGrid(position.X + rectangle.Width - 1) &&
        IsYInsideGrid(position.Y) && IsYInsideGrid(position.Y + rectangle.Height - 1);

    private bool IsXInsideGrid(int positionX) => positionX < Width;

    private bool IsYInsideGrid(int positionY) => positionY < Height;

    private bool ValidateRectangleDoesNotOverlap(PositionedRectangle candidatePositionedRectangle) =>
        _rectanglesInGrid.All(alreadyPositionedRectangle => !candidatePositionedRectangle.Overlaps(alreadyPositionedRectangle));

    public record PositionedRectangle
    {
        public Guid Id { get; }

        public Rectangle Rectangle { get; }

        public GridCoordinates Position { get; }

        private PositionedRectangle(Guid id, Rectangle rectangle, GridCoordinates position)
        {
            Id = id;
            Rectangle = rectangle;
            Position = position;
        }

        public PositionedRectangle(Rectangle rectangle, GridCoordinates position) : this(Guid.NewGuid(), rectangle, position) { }

        public bool Overlaps(PositionedRectangle anotherRectangle) => OverlapsX(anotherRectangle) && OverlapsY(anotherRectangle);

        private bool OverlapsX(PositionedRectangle anotherRectangle) =>
            Position.X + Rectangle.Width > anotherRectangle.Position.X &&
            Position.X < anotherRectangle.Position.X + anotherRectangle.Rectangle.Width;

        private bool OverlapsY(PositionedRectangle anotherRectangle) =>
            Position.Y + Rectangle.Height > anotherRectangle.Position.Y &&
            Position.Y < anotherRectangle.Position.Y + anotherRectangle.Rectangle.Height;
    }

    private static class ErrorMessages
    {
        public const string InvalidWidth = "Invalid Width: Width should be a positive integer.";
        public const string InvalidHeight = "Invalid Height: Height should be a positive integer.";
        public const string OutsideOfGrid = "The rectangle can't be outside of the grid.";
        public const string OverlappingRectangle = "The rectangle overlaps with an existing rectangle.";
        public static string NotFound(Guid rectangleId) => $"Couldn't find a rectangle with Id: {rectangleId}.";
    }
}

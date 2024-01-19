using PixelPlanner.Entities;
using System.Diagnostics;

namespace PixelPlanner.Api.Endpoints.Grids;

public record AddRectangleToGridRequest(int Width, int Height, int X, int Y);

public static class AddRectangleToGridRequestExtensions
{
    public static Result<(Rectangle, GridCoordinates)> ToDomain(this AddRectangleToGridRequest request) =>
        (Rectangle.Create(request.Width, request.Height), GridCoordinates.Create(request.X, request.Y)) switch
        {
            (FailedResult<Rectangle> rectangle, _) => Result<(Rectangle, GridCoordinates)>.Failed(rectangle.Error),
            (_, FailedResult<GridCoordinates> gridCoordinates) => Result<(Rectangle, GridCoordinates)>.Failed(gridCoordinates.Error),
            (SuccessfulResult<Rectangle> rectangle, SuccessfulResult<GridCoordinates> gridCoordinates) =>
                Result<(Rectangle, GridCoordinates)>.Successful((rectangle.Value, gridCoordinates.Value)),
            _ => throw new UnreachableException()
        };
}
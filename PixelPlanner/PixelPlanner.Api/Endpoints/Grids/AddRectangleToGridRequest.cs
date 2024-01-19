using PixelPlanner.Entities;
using System.Diagnostics;

namespace PixelPlanner.Api.Endpoints.Grids;

public record AddRectangleToGridRequest(int Width, int Height, int X, int Y);

public static class AddRectangleToGridRequestExtensions
{
    public static Result ToDomain(this AddRectangleToGridRequest request) =>
        (Rectangle.Create(request.Width, request.Height), GridCoordinates.Create(request.X, request.Y)) switch
        {
            (FailedResult rectangle, _) => Result.Failed(rectangle.Error),
            (_, FailedResult gridCoordinates) => Result.Failed(gridCoordinates.Error),
            (SuccessfulResult<Rectangle> rectangle, SuccessfulResult<GridCoordinates> gridCoordinates) =>
                Result.Successful((rectangle.Value, gridCoordinates.Value)),
            _ => throw new UnreachableException()
        };
}
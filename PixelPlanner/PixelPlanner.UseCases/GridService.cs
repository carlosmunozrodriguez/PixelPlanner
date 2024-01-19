using PixelPlanner.Entities;
using System.Diagnostics;
using Void = PixelPlanner.Entities.Void;

namespace PixelPlanner.UseCases;

public class GridService(IGridRepository gridRepository) : IGridService
{
    public Task<List<Grid>> GetAllGridsAsync() => gridRepository.GetAllGridsAsync();

    public Task<Grid?> GetGridByIdAsync(Guid gridId) => gridRepository.GetGridByIdAsync(gridId);

    public async Task<Result<Guid>> CreateGridAsync(int width, int height)
    {
        var createResult = Grid.CreateGrid(width, height);

        switch (createResult)
        {
            case SuccessfulResult<Grid> successfulResult:
                await gridRepository.CreateGridAsync(successfulResult.Value);
                return Result<Guid>.Successful(successfulResult.Value.Id);
            case FailedResult<Grid> failedResult:
                return Result<Guid>.Failed(failedResult.Error);
            default: throw new UnreachableException();
        }
    }

    public async Task<Result<Void>> DeleteGridAsync(Guid gridId)
    {
        var grid = await gridRepository.GetGridByIdAsync(gridId);

        if (grid is null)
        {
            return Result<Void>.Failed(ErrorMessages.GridNotFound);
        }

        await gridRepository.DeleteGridAsync(gridId);

        return Result<Void>.Successful(Void.Nothing);
    }

    public async Task<Result<Guid>> AddRectangleToGridAsync(Guid gridId, int width, int height, int x, int y)
    {
        var grid = await gridRepository.GetGridByIdAsync(gridId);

        if (grid is null)
        {
            return Result<Guid>.Failed(ErrorMessages.GridNotFound);
        }

        var rectangleResult = Rectangle.Create(width, height);
        switch (rectangleResult)
        {
            case FailedResult<Rectangle> failedResult: return Result<Guid>.Failed(failedResult.Error);
            case SuccessfulResult<Rectangle> successfulRectangleResult:
                var positionResult = GridCoordinates.Create(x, y);

                switch (positionResult)
                {
                    case FailedResult<GridCoordinates> failedResult: return Result<Guid>.Failed(failedResult.Error);
                    case SuccessfulResult<GridCoordinates> successfulPositionResult:
                        var position = successfulPositionResult.Value;
                        var rectangle = successfulRectangleResult.Value;
                        var result = grid.AddRectangle(rectangle, position);
                        if (result is SuccessfulResult<Guid> successfulAddRectangleResult)
                        {
                            await gridRepository.AddRectangleToGridAsync(gridId, rectangle, position, successfulAddRectangleResult.Value);
                        }

                        return result;
                    default: throw new UnreachableException();
                }
            default: throw new UnreachableException();
        }
    }

    public async Task<Result<Void>> RemoveRectangleFromGridAsync(Guid gridId, Guid positionedRectangleId)
    {
        var grid = await gridRepository.GetGridByIdAsync(gridId);

        if (grid is null)
        {
            return Result<Void>.Failed(ErrorMessages.GridNotFound);
        }

        var result = grid.RemoveRectangle(positionedRectangleId);
        if (result is SuccessfulResult<Void>)
        {
            await gridRepository.RemoveRectangleFromGridAsync(gridId, positionedRectangleId);
        }

        return result;
    }

    private static class ErrorMessages
    {
        public const string GridNotFound = "Grid not found.";
    }
}
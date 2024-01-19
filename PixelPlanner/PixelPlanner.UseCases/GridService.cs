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

    public async Task<Result<Guid>> AddRectangleToGridAsync(Guid gridId, Rectangle rectangle, GridCoordinates position)
    {
        var grid = await gridRepository.GetGridByIdAsync(gridId);

        if (grid is null)
        {
            return Result<Guid>.Failed(ErrorMessages.GridNotFound);
        }

        var result = grid.AddRectangle(rectangle, position);
        if (result is SuccessfulResult<Guid> successfulAddRectangleResult)
        {
            await gridRepository.AddRectangleToGridAsync(gridId, rectangle, position, successfulAddRectangleResult.Value);
        }

        return result;
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
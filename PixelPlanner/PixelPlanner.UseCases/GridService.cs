using PixelPlanner.Entities;
using Void = PixelPlanner.Entities.Void;

namespace PixelPlanner.UseCases;

public class GridService(IGridRepository gridRepository) : IGridService
{
    public Task<List<Grid>> GetAllGridsAsync() => gridRepository.GetAllGridsAsync();

    public Task<Grid?> GetGridByIdAsync(Guid gridId) => gridRepository.GetGridByIdAsync(gridId);

    public async Task<Result> CreateGridAsync(int width, int height)
    {
        var result = Grid.CreateGrid(width, height);

        if (result is not SuccessfulResult<Grid> successfulResult)
        {
            return result;
        }

        await gridRepository.CreateGridAsync(successfulResult.Value);
        return Result.Successful(successfulResult.Value.Id);

    }

    public async Task<Result> DeleteGridAsync(Guid gridId)
    {
        var grid = await gridRepository.GetGridByIdAsync(gridId);

        if (grid is null)
        {
            return Result.Failed(ErrorMessages.GridNotFound);
        }

        await gridRepository.DeleteGridAsync(gridId);

        return Result.Successful(Void.Nothing);
    }

    public async Task<Result> AddRectangleToGridAsync(Guid gridId, Rectangle rectangle, GridCoordinates position)
    {
        var grid = await gridRepository.GetGridByIdAsync(gridId);

        if (grid is null)
        {
            return Result.Failed(ErrorMessages.GridNotFound);
        }

        var result = grid.AddRectangle(rectangle, position);
        if (result is SuccessfulResult<Guid> successfulAddRectangleResult)
        {
            await gridRepository.AddRectangleToGridAsync(gridId, rectangle, position, successfulAddRectangleResult.Value);
        }

        return result;
    }

    public async Task<Result> RemoveRectangleFromGridAsync(Guid gridId, Guid positionedRectangleId)
    {
        var grid = await gridRepository.GetGridByIdAsync(gridId);

        if (grid is null)
        {
            return Result.Failed(ErrorMessages.GridNotFound);
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
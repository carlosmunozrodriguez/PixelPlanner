using PixelPlanner.Entities;
using Void = PixelPlanner.Entities.Void;

namespace PixelPlanner.UseCases;

public interface IGridService
{
    public Task<List<Grid>> GetAllGridsAsync();

    public Task<Grid?> GetGridByIdAsync(Guid gridId);

    public Task<Result<Guid>> CreateGridAsync(int width, int height);

    public Task<Result<Void>> DeleteGridAsync(Guid gridId);

    public Task<Result<Guid>> AddRectangleToGridAsync(Guid gridId, int width, int height, int x, int y);

    public Task<Result<Void>> RemoveRectangleFromGridAsync(Guid gridId, Guid positionedRectangleId);
}
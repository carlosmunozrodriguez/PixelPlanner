using PixelPlanner.Entities;

namespace PixelPlanner.UseCases;

public interface IGridService
{
    public Task<List<Grid>> GetAllGridsAsync();

    public Task<Grid?> GetGridByIdAsync(Guid gridId);

    public Task<Result> CreateGridAsync(int width, int height);

    public Task<Result> DeleteGridAsync(Guid gridId);

    public Task<Result> AddRectangleToGridAsync(Guid gridId, Rectangle rectangle, GridCoordinates position);

    public Task<Result> RemoveRectangleFromGridAsync(Guid gridId, Guid positionedRectangleId);
}
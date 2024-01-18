using PixelPlanner.Entities;

namespace PixelPlanner.UseCases;

public interface IGridRepository
{
    public Task<List<Grid>> GetAllGridsAsync();

    public Task CreateGridAsync(Grid grid);

    public Task<Grid?> GetGridByIdAsync(Guid gridId);

    public Task DeleteGridAsync(Guid gridId);

    public Task AddRectangleToGridAsync(Guid gridId, Rectangle rectangle, GridCoordinates position, Guid positionedRectangleId);

    public Task RemoveRectangleFromGridAsync(Guid gridId, Guid positionedRectangleId);
}
using PixelPlanner.Entities;
using PixelPlanner.UseCases;

namespace PixelPlanner.Persistence;

public class InMemoryGridRepository : IGridRepository
{
    private static readonly List<Grid> Grids = [];

    public Task<List<Grid>> GetAllGridsAsync() => Task.FromResult(Grids);

    public Task<Grid?> GetGridByIdAsync(Guid gridId) => Task.FromResult(Grids.Find(x => x.Id == gridId));

    public Task CreateGridAsync(Grid grid)
    {
        Grids.Add(grid);
        return Task.CompletedTask;
    }

    public Task DeleteGridAsync(Guid gridId)
    {
        Grids.RemoveAll(x => x.Id == gridId);
        return Task.CompletedTask;
    }

    public Task AddRectangleToGridAsync(Guid gridId, Rectangle rectangle, GridCoordinates position, Guid positionedRectangleId) => Task.CompletedTask;

    public Task RemoveRectangleFromGridAsync(Guid gridId, Guid positionedRectangleId) => Task.CompletedTask;
}
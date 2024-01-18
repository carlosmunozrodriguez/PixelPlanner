using Microsoft.AspNetCore.Mvc;
using PixelPlanner.Entities;
using PixelPlanner.UseCases;
using System.Diagnostics;
using Void = PixelPlanner.Entities.Void;

namespace PixelPlanner.Api.Endpoints.Grids;

[ApiController]
[Route("grids")]
public class GridController(IGridService gridService) : ControllerBase
{
    [HttpGet(Name = nameof(GetAllGrids))]
    public async Task<ActionResult<List<Grid>>> GetAllGrids() => await gridService.GetAllGridsAsync();

    [HttpGet("{gridId:guid}", Name = nameof(GetGrid))]
    public async Task<ActionResult<Grid>> GetGrid(Guid gridId)
    {
        var grid = await gridService.GetGridByIdAsync(gridId);

        if (grid is null)
        {
            return NotFound();
        }

        return grid;
    }

    [HttpPost(Name = nameof(CreateGrid))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateGrid(CreateGridRequest request)
    {
        var result = await gridService.CreateGridAsync(request.Height, request.Width);

        return result switch
        {
            FailedResult<Guid> failedResult => Problem(failedResult.Error, statusCode: StatusCodes.Status400BadRequest),
            SuccessfulResult<Guid> successfulResult => CreatedAtAction(nameof(GetGrid), new { gridId = successfulResult.Value }, null),
            _ => throw new UnreachableException()
        };
    }

    [HttpDelete("{gridId:guid}", Name = nameof(DeleteGrid))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteGrid(Guid gridId)
    {
        var result = await gridService.DeleteGridAsync(gridId);

        if (result is FailedResult<Void> failedResult)
        {
            return Problem(failedResult.Error, statusCode: StatusCodes.Status400BadRequest);
        }

        return NoContent();
    }

    [HttpPost("{gridId:guid}/rectangles", Name = nameof(AddRectangleToGrid))]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AddRectangleToGridResponse>> AddRectangleToGrid(Guid gridId, AddRectangleToGridRequest request)
    {
        var result = await gridService.AddRectangleToGridAsync(gridId, request.Width, request.Height, request.X, request.Y);

        return result switch
        {
            FailedResult<Guid> failedResult => Problem(failedResult.Error, statusCode: StatusCodes.Status400BadRequest),
            SuccessfulResult<Guid> successfulResult => new AddRectangleToGridResponse(successfulResult.Value),
            _ => throw new UnreachableException()
        };
    }

    [HttpDelete("{gridId:guid}/rectangles/{rectangleId:guid}", Name = nameof(RemoveRectangleFromGrid))]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> RemoveRectangleFromGrid(Guid gridId, Guid rectangleId)
    {
        var result = await gridService.RemoveRectangleFromGridAsync(gridId, rectangleId);

        if (result is FailedResult<Void> failedResult)
        {
            return Problem(failedResult.Error, statusCode: StatusCodes.Status400BadRequest);
        }

        return NoContent();
    }
}
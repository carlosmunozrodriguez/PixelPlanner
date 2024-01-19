namespace PixelPlanner.Entities.UnitTests;

public class GridTests
{
    private const int ValidLength = 1;
    private const int NegativeLength = -1;
    private const int ZeroLength = 0;

    [Theory]
    [InlineData(NegativeLength, ValidLength)]
    [InlineData(ZeroLength, ValidLength)]
    [InlineData(ValidLength, NegativeLength)]
    [InlineData(ValidLength, ZeroLength)]
    public void Given_InvalidLengths_When_CreatingGrid_Then_FailsToCreate(int width, int height)
    {
        var result = Grid.CreateGrid(width, height);

        Assert.False(result.Success);
        Assert.IsType<FailedResult>(result);
    }

    [Fact]
    public void Given_ValidLengths_When_CreatingGrid_Then_ReturnsGrid()
    {
        var result = Grid.CreateGrid(ValidLength, ValidLength);

        Assert.True(result.Success);
        var successfulResult = Assert.IsType<SuccessfulResult<Grid>>(result);
        Assert.NotNull(successfulResult.Value);
    }

    [Theory]
    [InlineData(8, 5)]
    [InlineData(10, 5)]
    [InlineData(12, 5)]
    [InlineData(5, 8)]
    [InlineData(5, 10)]
    [InlineData(5, 12)]
    [InlineData(8, 8)]
    [InlineData(8, 10)]
    [InlineData(8, 12)]
    public void Given_EmptyGrid_When_AddingRectangleOutsideLimits_Then_FailsToAdd(int x, int y)
    {
        var grid = Create9X9EmptyGrid();
        var rectangle = Create3X3Rectangle();
        var positionResult = GridCoordinates.Create(x, y);
        var successfulResult = Assert.IsType<SuccessfulResult<GridCoordinates>>(positionResult);

        var result = grid.AddRectangle(rectangle, successfulResult.Value);

        Assert.False(result.Success);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(3, 0)]
    [InlineData(6, 0)]
    [InlineData(0, 3)]
    [InlineData(3, 3)]
    [InlineData(6, 3)]
    [InlineData(0, 6)]
    [InlineData(3, 6)]
    [InlineData(6, 6)]
    public void Given_EmptyGrid_When_AddingRectangleInsideLimits_Then_SuccessfullyAdds(int x, int y)
    {
        var grid = Create9X9EmptyGrid();
        var rectangle = Create3X3Rectangle();
        var positionResult = GridCoordinates.Create(x, y);
        var successfulCreateResult = Assert.IsType<SuccessfulResult<GridCoordinates>>(positionResult);

        var result = grid.AddRectangle(rectangle, successfulCreateResult.Value);

        Assert.True(result.Success);
        var successfulResult = Assert.IsType<SuccessfulResult<Guid>>(result);
        Assert.NotEqual(Guid.Empty, successfulResult.Value);
    }

    [Theory]
    [InlineData(2, 2)]
    [InlineData(3, 2)]
    [InlineData(4, 2)]
    [InlineData(6, 2)]
    [InlineData(7, 2)]
    [InlineData(8, 2)]
    [InlineData(10, 2)]
    [InlineData(11, 2)]
    [InlineData(12, 2)]

    [InlineData(2, 3)]
    [InlineData(3, 3)]
    [InlineData(4, 3)]
    [InlineData(6, 3)]
    [InlineData(7, 3)]
    [InlineData(8, 3)]
    [InlineData(10, 3)]
    [InlineData(11, 3)]
    [InlineData(12, 3)]

    [InlineData(2, 4)]
    [InlineData(3, 4)]
    [InlineData(11, 4)]
    [InlineData(12, 4)]

    [InlineData(2, 6)]
    [InlineData(3, 6)]
    [InlineData(11, 6)]
    [InlineData(12, 6)]

    [InlineData(2, 7)]
    [InlineData(3, 7)]
    [InlineData(11, 7)]
    [InlineData(12, 7)]

    [InlineData(2, 8)]
    [InlineData(3, 8)]
    [InlineData(11, 8)]
    [InlineData(12, 8)]

    [InlineData(2, 10)]
    [InlineData(3, 10)]
    [InlineData(11, 10)]
    [InlineData(12, 10)]

    [InlineData(2, 11)]
    [InlineData(3, 11)]
    [InlineData(4, 11)]
    [InlineData(6, 11)]
    [InlineData(7, 11)]
    [InlineData(8, 11)]
    [InlineData(10, 11)]
    [InlineData(11, 11)]
    [InlineData(12, 11)]

    [InlineData(2, 12)]
    [InlineData(3, 12)]
    [InlineData(4, 12)]
    [InlineData(6, 12)]
    [InlineData(7, 12)]
    [InlineData(8, 12)]
    [InlineData(10, 12)]
    [InlineData(11, 12)]
    [InlineData(12, 12)]


    public void Given_GridWithOneRectangle_When_AddingNonOverlappingRectangle_Then_SuccessfullyAdds(int x, int y)
    {
        var grid = Create17X17GridWith5X5Rectangle();
        var rectangle = Create3X3Rectangle();
        var positionResult = GridCoordinates.Create(x, y);
        var successfulCreateResult = Assert.IsType<SuccessfulResult<GridCoordinates>>(positionResult);

        var result = grid.AddRectangle(rectangle, successfulCreateResult.Value);

        Assert.True(result.Success);
        var successfulResult = Assert.IsType<SuccessfulResult<Guid>>(result);
        Assert.NotEqual(Guid.Empty, successfulResult.Value);
    }

    [Theory]
    [InlineData(4, 4)]
    [InlineData(6, 4)]
    [InlineData(7, 4)]
    [InlineData(8, 4)]
    [InlineData(10, 4)]

    [InlineData(4, 6)]
    [InlineData(6, 6)]
    [InlineData(7, 6)]
    [InlineData(8, 6)]
    [InlineData(10, 6)]

    [InlineData(4, 7)]
    [InlineData(6, 7)]
    [InlineData(7, 7)]
    [InlineData(8, 7)]
    [InlineData(10, 7)]

    [InlineData(4, 8)]
    [InlineData(6, 8)]
    [InlineData(7, 8)]
    [InlineData(8, 8)]
    [InlineData(10, 8)]

    [InlineData(4, 10)]
    [InlineData(6, 10)]
    [InlineData(7, 10)]
    [InlineData(8, 10)]
    [InlineData(10, 10)]

    public void Given_GridWithOneRectangle_When_AddingOverlappingRectangle_Then_FailsToAdds(int x, int y)
    {
        var grid = Create17X17GridWith5X5Rectangle();
        var rectangle = Create3X3Rectangle();
        var positionResult = GridCoordinates.Create(x, y);
        var successfulCreateResult = Assert.IsType<SuccessfulResult<GridCoordinates>>(positionResult);

        var result = grid.AddRectangle(rectangle, successfulCreateResult.Value);

        Assert.False(result.Success);
    }

    [Fact]
    public void Given_EmptyGrid_When_RemovingRectangle_Then_AlwaysFail()
    {
        var grid = Create9X9EmptyGrid();

        var result = grid.RemoveRectangle(Guid.NewGuid());

        Assert.False(result.Success);
    }

    [Fact]
    public void Given_GridWithOneRectangle_When_RemovingThatRectangle_Then_SuccessfullyRemovesIt()
    {
        var grid = Create9X9EmptyGrid();
        var rectangle = Create3X3Rectangle();
        var positionResult = GridCoordinates.Create(3, 3);
        var successfulCreateResult = Assert.IsType<SuccessfulResult<GridCoordinates>>(positionResult);
        var addResult = grid.AddRectangle(rectangle, successfulCreateResult.Value);
        var successfulResult = Assert.IsType<SuccessfulResult<Guid>>(addResult);
        var newRectangleId = successfulResult.Value;

        var result = grid.RemoveRectangle(newRectangleId);

        Assert.True(result.Success);
    }

    [Fact]
    public void Given_GridWithOneRectangle_When_RemovingANonExistentRectangle_Then_FailsToRemove()
    {
        var grid = Create9X9EmptyGrid();
        var rectangle = Create3X3Rectangle();
        var positionResult = GridCoordinates.Create(3, 3);
        var successfulCreateResult = Assert.IsType<SuccessfulResult<GridCoordinates>>(positionResult);
        grid.AddRectangle(rectangle, successfulCreateResult.Value);

        var result = grid.RemoveRectangle(Guid.NewGuid());

        Assert.False(result.Success);
    }

    private static Grid Create9X9EmptyGrid() => ((SuccessfulResult<Grid>)Grid.CreateGrid(9, 9)).Value;

    private static Grid Create17X17GridWith5X5Rectangle()
    {
        var grid = ((SuccessfulResult<Grid>)Grid.CreateGrid(17, 17)).Value;
        var rectangle = ((SuccessfulResult<Rectangle>)Rectangle.Create(5, 5)).Value;
        var position = ((SuccessfulResult<GridCoordinates>)GridCoordinates.Create(6, 6)).Value;
        grid.AddRectangle(rectangle, position);
        return grid;
    }

    private static Rectangle Create3X3Rectangle() => ((SuccessfulResult<Rectangle>)Rectangle.Create(3, 3)).Value;

}
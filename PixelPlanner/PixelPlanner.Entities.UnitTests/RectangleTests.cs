namespace PixelPlanner.Entities.UnitTests;

public class RectangleTests
{
    private const int ValidLength = 1;
    private const int NegativeLength = -1;
    private const int ZeroLength = 0;

    [InlineData(NegativeLength, ValidLength)]
    [InlineData(ZeroLength, ValidLength)]
    [InlineData(ValidLength, NegativeLength)]
    [InlineData(ValidLength, ZeroLength)]
    [Theory]
    public void Given_InvalidLengths_When_CreatingRectangle_Then_FailsToCreate(int width, int height)
    {
        var squareResult = Rectangle.Create(width, height);

        Assert.IsType<FailedResult>(squareResult);
    }

    [Fact]
    public void Given_ValidLengths_When_CreatingRectangle_Then_ReturnsRectangle()
    {
        var squareResult = Rectangle.Create(ValidLength, ValidLength);

        var square = Assert.IsType<SuccessfulResult<Rectangle>>(squareResult);
        Assert.NotNull(square);
    }
}
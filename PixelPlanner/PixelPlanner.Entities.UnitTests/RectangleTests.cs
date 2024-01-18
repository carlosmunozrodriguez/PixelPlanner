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
    public void Given_InvalidLengths_When_CallingConstructor_Then_ThrowsArgumentOutOfRangeException(int width, int height)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Rectangle(width, height));
    }

    [Fact]
    public void Given_ValidLengths_When_CallingConstructor_Then_ReturnsRectangle()
    {
        var square = new Rectangle(ValidLength, ValidLength);

        Assert.NotNull(square);
    }
}
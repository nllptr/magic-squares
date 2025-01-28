using MagicSquaresApi;
using Xunit;

namespace MagicSquaresApi.Tests;

public class SquaresTests
{
    [Fact]
    public void TestNewState()
    {
        var squares = new Squares();
        Assert.Empty(squares.GetAllSquares());
    }

    [Fact]
    public void TestAddSquare()
    {
        var squares = new Squares();
        squares.AddSquare("#FF0000");
        Assert.Single(squares.GetAllSquares());
    }

    [Fact]
    public void TestAddInvalidSquare()
    {
        var squares = new Squares();
        Assert.Throws<ArgumentException>(() => squares.AddSquare("invalid"));
    }
}

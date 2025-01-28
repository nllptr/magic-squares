using MagicSquaresApi;
using Xunit;
using System.Collections.Generic;
using System.Text.Json;

namespace MagicSquaresApi.Tests
{
    public class SquaresTests
    {
        [Fact]
        public void GetAllSquares_ReturnsEmptyList_WhenNoSquaresAdded()
        {
            // Arrange
            var fileService = new InMemoryFileService();
            var squares = new Squares(fileService);

            // Act
            var result = squares.GetAllSquares();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void AddSquare_AddsSquareToList()
        {
            // Arrange
            var fileService = new InMemoryFileService();
            var squares = new Squares(fileService);
            var hexColor = "#FFFFFF";

            // Act
            squares.AddSquare(hexColor);
            var result = squares.GetAllSquares();

            // Assert
            Assert.Contains(hexColor, result);
        }

        [Fact]
        public void AddSquare_ThrowsArgumentException_ForInvalidHexColor()
        {
            // Arrange
            var fileService = new InMemoryFileService();
            var squares = new Squares(fileService);
            var invalidHexColor = "invalid";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => squares.AddSquare(invalidHexColor));
        }

        [Fact]
        public void GetAllSquares_CachesResult_AfterFirstCall()
        {
            // Arrange
            var fileService = new InMemoryFileService();
            var hexColor = "#FFFFFF";
            fileService.WriteLine("squares.json", JsonSerializer.Serialize(hexColor));
            var squares = new Squares(fileService);

            // Act
            var result1 = squares.GetAllSquares();
            var result2 = squares.GetAllSquares();

            // Assert
            Assert.Same(result1, result2); // Ensure the same cached result is returned
        }

        [Fact]
        public void AddSquare_MarksCacheAsDirty()
        {
            // Arrange
            var fileService = new InMemoryFileService();
            var hexColor1 = "#FFFFFF";
            var hexColor2 = "#000000";
            fileService.WriteLine("squares.json", JsonSerializer.Serialize(hexColor1));
            var squares = new Squares(fileService);

            // Act
            var result1 = squares.GetAllSquares();
            squares.AddSquare(hexColor2);
            var result2 = squares.GetAllSquares();

            // Assert
            Assert.NotSame(result1, result2); // Ensure the cache is refreshed
        }

        [Fact]
        public void AddSquare_ThrowsArgumentException_WhenRepeatingSquare()
        {
            // Arrange
            var fileService = new InMemoryFileService();
            var hexColor = "#FFFFFF";
            var squares = new Squares(fileService);
            squares.AddSquare(hexColor);

            // Act & Assert
            // Ensure the same square cannot be added twice in succession
            Assert.Throws<ArgumentException>(() => squares.AddSquare(hexColor));
        }

        [Fact]
        public void AddSquare_AllowsRepeating_IfNotSuccession()
        {
            // Arrange
            var fileService = new InMemoryFileService();
            var hexColor1 = "#FFFFFF";
            var hexColor2 = "#000000";
            var squares = new Squares(fileService);

            // Act
            // Ensure it's OK to repeat hex codes as long as they're not in succession
            squares.AddSquare(hexColor1);
            squares.AddSquare(hexColor2);
            squares.AddSquare(hexColor1);

            // Assert
            // Ensure the same square cannot be added twice in succession
            Assert.Equal(new[] { hexColor1, hexColor2, hexColor1 }, squares.GetAllSquares());
        }
    }

    public class InMemoryFileService : IFileService
    {
        private readonly Dictionary<string, List<string>> _fileSystem = new();

        public bool Exists(string path)
        {
            return _fileSystem.ContainsKey(path);
        }

        public string[] ReadAllLines(string path)
        {
            if (!_fileSystem.ContainsKey(path))
            {
                throw new FileNotFoundException();
            }
            return _fileSystem[path].ToArray();
        }

        public void WriteLine(string path, string content)
        {
            if (!_fileSystem.ContainsKey(path))
            {
                _fileSystem[path] = new List<string>();
            }
            _fileSystem[path].Add(content);
        }
    }   
}
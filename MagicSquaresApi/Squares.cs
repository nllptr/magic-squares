using System.Text.RegularExpressions;
using System.Text.Json;

namespace MagicSquaresApi
{
    public class Squares
    {
        private const string FilePath = "squares.json";
        private readonly IFileService _fileService;
        private List<string> _cachedSquares = [];
        private bool _isCacheDirty = true;
        private string ? _lastSquare;

        public Squares(IFileService fileService)
        {
            _fileService = fileService;
        }

        public IEnumerable<string> GetAllSquares()
        {
            if (_isCacheDirty)
            {
                _cachedSquares = LoadSquaresFromFile();
                _isCacheDirty = false;
            }
            return _cachedSquares;
        }

        public void AddSquare(string hexColor)
        {
            if (!IsValidHexColor(hexColor)) throw new ArgumentException("Invalid hex color code", nameof(hexColor));
            if (_lastSquare == hexColor) throw new ArgumentException("Duplicate hex color code", nameof(hexColor));

            AppendSquareToFile(hexColor);
            _isCacheDirty = true;
            _lastSquare = hexColor;
        }

        private static bool IsValidHexColor(string hexColor)
        {
            if (string.IsNullOrEmpty(hexColor))
            {
                return false;
            }

            var regex = new Regex("^#([0-9A-Fa-f]{6}|[0-9A-Fa-f]{3})$");
            return regex.IsMatch(hexColor);
        }

        private List<string> LoadSquaresFromFile()
        {
            if (!_fileService.Exists(FilePath))
            {
                return [];
            }

            var lines = _fileService.ReadAllLines(FilePath);
            var squares = new List<string>();
            foreach (var line in lines)
            {
                var hexColor = JsonSerializer.Deserialize<string>(line);
                if (hexColor != null)
                {
                    squares.Add(hexColor);
                }
            }
            return squares;
        }

        private void AppendSquareToFile(string hexColor)
        {
            _fileService.WriteLine(FilePath, JsonSerializer.Serialize(hexColor));
        }
    }
}
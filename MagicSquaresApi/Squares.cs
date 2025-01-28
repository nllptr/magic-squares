using System.Text.RegularExpressions;

namespace MagicSquaresApi
{
    public class Squares
    {
        private List<string> _squares = new List<string>();

        public IEnumerable<string> GetAllSquares()
        {
            return _squares;
        }

        public void AddSquare(string hexColor)
        {
            if (IsValidHexColor(hexColor))
            {
                _squares.Add(hexColor);
            }
            else
            {
                throw new ArgumentException("Invalid hex color code", nameof(hexColor));
            }
        }

        private bool IsValidHexColor(string hexColor)
        {
            if (string.IsNullOrEmpty(hexColor))
            {
                return false;
            }

            // Regex to match hex color codes like #FFFFFF or #FFF
            var regex = new Regex("^#([0-9A-Fa-f]{6}|[0-9A-Fa-f]{3})$");
            return regex.IsMatch(hexColor);
        }
    }
}

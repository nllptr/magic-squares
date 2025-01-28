using System.Collections.Generic;

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
            _squares.Add(hexColor);
        }
    }
}

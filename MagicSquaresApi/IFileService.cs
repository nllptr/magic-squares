namespace MagicSquaresApi
{
    public interface IFileService
    {
        bool Exists(string path);
        string[] ReadAllLines(string path);
        void WriteLine(string path, string content);
    }
}
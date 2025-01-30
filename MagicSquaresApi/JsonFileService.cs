using System.IO;
using System.Text.Json;

namespace MagicSquaresApi
{
    public class JsonFileService : IFileService
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public string[] ReadAllLines(string path)
        {
            if (!File.Exists(path))
            {
                return [];
            }

            var json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<string[]>(json) ?? [];
        }

        public void WriteLine(string path, string content)
        {
            string[] existingLines = new string[0];
            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                existingLines = JsonSerializer.Deserialize<string[]>(json) ?? new string[0];
            }

            var updatedLines = new List<string>(existingLines) { content };
            var updatedJson = JsonSerializer.Serialize(updatedLines);
            File.WriteAllText(path, updatedJson);
        }
    }
}
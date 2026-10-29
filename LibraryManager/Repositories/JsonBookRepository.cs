

using LibraryManager.Models;
using System.Text.Json;

namespace LibraryManager.Repositories
{
    public class JsonBookRepository : IBookRepository
    {
        private readonly string _filePath;
        public JsonBookRepository(string filePath)
        {
            _filePath = filePath;
        }
        public IReadOnlyList<Book> GetAll()
        {
            if (File.Exists(_filePath)) { 
            var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
            }
            return new List<Book>();
        }

        public void Save(IEnumerable<Book> books)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(_filePath)!);
            var json = JsonSerializer.Serialize(books);
            File.WriteAllText(_filePath, json);
        }
    }
}

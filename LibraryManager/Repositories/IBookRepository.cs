using LibraryManager.Models;

namespace LibraryManager.Repositories
{
    public interface IBookRepository
    {
        IReadOnlyList<Book>GetAll();
        void Save(IEnumerable<Book>books);
    }
}

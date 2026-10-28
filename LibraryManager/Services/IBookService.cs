using LibraryManager.Models;

namespace LibraryManager.Services
{
    public interface IBookService
    {
        void AddBook(Book book);
        bool RemoveBook(string id);
        IEnumerable<Book> SearchByAuthor(string author);
        IEnumerable<Book> SearchByTitle(string title);
        bool BorrowBook(string id);
        bool ReturnBook(string id);
    }
}

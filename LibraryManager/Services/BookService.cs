using LibraryManager.Models;
using LibraryManager.Repositories;

namespace LibraryManager.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly Dictionary<string, Book> _books;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
            _books=bookRepository.GetAll().ToDictionary(b => b.Id, b => b);    
        }
        public void AddBook(Book book)
        {
            _books[book.Id] = book;
            _bookRepository.Save(_books.Values);
        }

        public bool BorrowBook(string id)
        {
            if (_books.TryGetValue(id, out Book book) && book.IsAvailable)
            {
                book.IsAvailable = false;
                _bookRepository.Save(_books.Values);
                return true;
            }
            return false;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _books.Values.ToList();
        }

        public bool RemoveBook(string id)
        {
            if (_books.TryGetValue(id, out Book book))
            {
                _books.Remove(id);
                _bookRepository.Save(_books.Values);
                return true;
            }
            return false;
        }          

        public bool ReturnBook(string id)
        {
            if (_books.TryGetValue(id, out Book book) && !book.IsAvailable)
            {
                book.IsAvailable = true;
                _bookRepository.Save(_books.Values);
                return true;
            }
            return false;
        }

        public IEnumerable<Book> SearchByAuthor(string author)
        {
            return _books.Values.Where(b => b.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
        }

        public IEnumerable<Book> SearchByTitle(string title)
        {
            return _books.Values.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase));
        }
    }
}

using LibraryManager.Models;
using LibraryManager.Services;

namespace LibraryManager.UI
{
    public class ConsoleMenu
    {
        private readonly IBookService _bookService;
        public ConsoleMenu(IBookService bookService)
        {
            _bookService = bookService;
        }
        private void PrintMenu()
        {
            Console.WriteLine("Library Management System");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Remove Book");
            Console.WriteLine("3. Search by Author");
            Console.WriteLine("4. Search by Title");
            Console.WriteLine("5. Borrow Book");
            Console.WriteLine("6. Return Book");
            Console.WriteLine("7. List All Books");
            Console.WriteLine("0. Exit");
        }
        public void Run()
        {
            while (true)
            {
                PrintMenu();
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        RemoveBook();
                        break;
                    case "3":
                        SearchByAuthor();
                        break;
                    case "4":
                        SearchByTitle();
                        break;
                    case "5":
                        BorrowBook();
                        break;
                    case "6":
                        ReturnBook();
                        break;
                    case "7":
                        ListAllBooks();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void ListAllBooks()
        {
            _bookService.GetAllBooks().Where(b=>b.IsAvailable).ToList().ForEach(book =>
            {
                Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Status: {book.Status}");
            });
        }

        private void ReturnBook()
        {
            Console.Write("Enter ID: ");
            var result = _bookService.ReturnBook(Console.ReadLine());
            Console.WriteLine(result ? "Book returned." : "Book not found or already available.");
        }

        private void BorrowBook()
        {
            Console.Write("Enter ID: ");
            var result = _bookService.BorrowBook(Console.ReadLine());
            Console.WriteLine(result ? "Book borrowed." : "Book not found or not available.");
        }

        private void SearchByTitle()
        {
            Console.Write("Enter title: ");
            _bookService.SearchByTitle(Console.ReadLine()).ToList().ForEach(book =>
            {
                Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Status: {book.Status}");
            });
        }

        private void SearchByAuthor()
        {
            Console.Write("Enter author: ");
            _bookService.SearchByAuthor(Console.ReadLine()).ToList().ForEach(book =>
            {
                Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}, Status: {book.Status}");
            });
        }

        private void RemoveBook()
        {
            Console.Write("Enter ID: ");
            var result = _bookService.RemoveBook(Console.ReadLine());
            Console.WriteLine(result ? "Book removed." : "Book not found.");
        }

        private void AddBook()
        {
            Console.Write("Title: ");
            var title = Console.ReadLine();
            Console.Write("Author: ");
            var author = Console.ReadLine();
            Console.Write("Year: ");
            int.TryParse(Console.ReadLine(), out var year);
            Console.Write("ID: ");
            var id = Console.ReadLine();

            _bookService.AddBook(new Book { Id = id, Title = title, Author = author, Year = year });
            Console.WriteLine("Book added.");
        }
    }
}

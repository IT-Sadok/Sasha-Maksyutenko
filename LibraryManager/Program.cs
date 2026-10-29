using LibraryManager.Repositories;
using LibraryManager.Services;
using LibraryManager.UI;
var dataPath=Path.Combine(AppContext.BaseDirectory, "data", "books.json");
var repository = new JsonBookRepository(dataPath);
var service = new BookService(repository);
var menu=new ConsoleMenu(service);
menu.Run();
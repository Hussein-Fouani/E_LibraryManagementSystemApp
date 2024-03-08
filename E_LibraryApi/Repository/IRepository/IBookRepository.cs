using ELibrary.Domain.Models;
using System.Linq.Expressions;

namespace E_LibraryApi.Repository.IRepository
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooks();
        Task<Book> GetBook(Expression<Func<Book, bool>> filter = null);
        Task<bool> BookExists (string bookName);
        Task CreateBook(Book book);
        Task UpdateBook(Book book);
        Task UpdateBookAvailability(string bookId, bool isAvailable);
        Task DeleteBook(Book book);
        Task Save(); 
    }
}

using E_LibraryApi.Repository.IRepository;
using E_LibraryManagementSystem.Db;
using ELibrary.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_LibraryApi.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly E_LibDb db;
        
        public BookRepository(E_LibDb db)
        {
            this.db = db;

        }
        public async Task<bool> BookExists(string bookName)
        {
            if (!string.IsNullOrEmpty(bookName))
            {
                var bookexits = await db.Book.FirstOrDefaultAsync(u => u.BookName == bookName);
                return bookexits != null;
            }
            return false;


        }

        public async Task CreateBook(Book book)
        {
            await db.Book.AddAsync(book);
            await Save();
        }

        public async Task DeleteBook(Book bookId)
        {
            db.Book.Remove(bookId);
            await Save();
        }

        public async Task<Book> GetBook(Expression<Func<Book, bool>> filter = null)
        {
            IQueryable<Book> books = db.Book;
            
            if (filter != null)
            {
                books = books.Where(filter);
            }
            return await books.FirstOrDefaultAsync();

        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await db.Book.ToListAsync();
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

        public async Task UpdateBook(Book book)
        {
            db.Book.Update(book);
            await Save();
        }
        public async Task BorrowBook(Book book)
        {
           var b= await db.FindAsync<Book>(book);
            b.IsAvailable = false;
            await Save();
        }

        public async Task UpdateBookAvailability(string bookId, bool isAvailable)
        {
            var book = await db.Book.FirstOrDefaultAsync(u => u.BookName == bookId);
            book.IsAvailable = isAvailable;
            await Save();
        }
    }
}

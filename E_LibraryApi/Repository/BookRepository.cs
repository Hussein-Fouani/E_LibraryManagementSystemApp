using E_LibraryApi.Models;
using E_LibraryApi.Models.APIResponse;
using E_LibraryApi.Repository.IRepository;
using E_LibraryManagementSystem.Db;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_LibraryApi.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly E_LibDb db;
        protected ApiReponse apiReponse;
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

        public async Task CreateBook(BookModel book)
        {
            await db.Book.AddAsync(book);
            await Save();
        }

        public async Task DeleteBook(BookModel bookId)
        {
            db.Book.Remove(bookId);
            await Save();
        }

        public async Task<BookModel> GetBook(Expression<Func<BookModel, bool>> filter = null)
        {
            IQueryable<BookModel> books = db.Book;
            
            if (filter != null)
            {
                books = books.Where(filter);
            }
            return await books.FirstOrDefaultAsync();

        }

        public async Task<List<BookModel>> GetAllBooks()
        {
            return await db.Book.ToListAsync();
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

        public async Task UpdateBook(BookModel book)
        {
            db.Book.Update(book);
            await Save();
        }
    }
}

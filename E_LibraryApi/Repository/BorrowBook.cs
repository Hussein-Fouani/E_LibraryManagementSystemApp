using E_LibraryApi.Repository.IRepository;
using E_LibraryManagementSystem.Db;
using ELibrary.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace E_LibraryApi.Repository
{
    public class BorrowBook : IBorrowBook
    {
        private readonly E_LibDb db;

        public BorrowBook(E_LibDb db)
        {
            this.db = db;
        }
        public async Task CreateBorrowBook(BorrowedBooks borrowedBooks)
        {
            await db.BorrowedBooks.AddAsync(borrowedBooks);
            await db.SaveChangesAsync();
        }

        public async Task DeleteBorrowBook(BorrowedBooks borrowedBooks)
        {
            db.BorrowedBooks.Remove(borrowedBooks);
            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<BorrowedBooks>> GetAllBorrowBook()
        {
          return await  db.BorrowedBooks.Include(bb => bb.Book).Include(bb => bb.User).ToListAsync();
            
        }

        public async Task<BorrowedBooks> GetBorrowBook(Guid borrowBookId)
        {
            return await db.BorrowedBooks.FirstOrDefaultAsync(x => x.BorrowBookId == borrowBookId);
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

        public async Task UpdateBorrowBook(BorrowedBooks borrowedBooks, Guid Id)
        {
            await db.SaveChangesAsync();
        }
    }
}

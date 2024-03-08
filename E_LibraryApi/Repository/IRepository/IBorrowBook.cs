using ELibrary.Domain.Models;

namespace E_LibraryApi.Repository.IRepository
{
    public interface IBorrowBook
    {
        Task CreateBorrowBook(BorrowedBooks borrowedBooks);
        Task DeleteBorrowBook(BorrowedBooks borrowedBooks);
        Task UpdateBorrowBook(BorrowedBooks borrowedBooks,Guid Id);
        Task<BorrowedBooks> GetBorrowBook(Guid borrowBookId);
        Task<IEnumerable<BorrowedBooks>> GetAllBorrowBook();
        Task Save();
    }
}

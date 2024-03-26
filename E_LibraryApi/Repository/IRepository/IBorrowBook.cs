using ELibrary.Domain.Models;

namespace E_LibraryApi.Repository.IRepository
{
    public interface IBorrowBook
    {
        Task CreateBorrowBook(BorrowedBooks borrowedBooks);
        Task<bool> UserHasBorrowedBook(Guid userId, Guid bookId);
        Task<IEnumerable<BorrowedBooks>> GetAllBorrowBook();
        Task Save();
    }
}

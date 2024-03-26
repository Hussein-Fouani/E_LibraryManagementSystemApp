using E_LibraryApi.Repository.IRepository;
using E_LibraryManagementSystem.Db;
using ELibrary.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace E_LibraryApi.Repository
{
    public class BorrowBook : IBorrowBook
    {
        private readonly E_LibDb db;

        /// <summary>
        /// Constructor for BorrowBook class.
        /// </summary>
        /// <param name="db">Instance of the database context.</param>
        public BorrowBook(E_LibDb db)
        {
            this.db = db;
        }

        /// <summary>
        /// Creates a borrow record for a book.
        /// </summary>
        /// <param name="borrowedBooks">The borrow record to be created.</param>
        /// <exception cref="Exception">Thrown if user has already borrowed the book or no copies are available.</exception>
        public async Task CreateBorrowBook(BorrowedBooks borrowedBooks)
        {
            try
            {
                var existingBorrow = await db.BorrowedBooks
                    .FirstOrDefaultAsync(b => b.BookId == borrowedBooks.BookId && b.UserId == borrowedBooks.UserId);

                if (existingBorrow != null)
                {
                    throw new Exception("User has already borrowed this book.");
                }

                var book = await db.Book.FirstOrDefaultAsync(b => b.Id == borrowedBooks.BookId);

                if (book != null)
                {
                    if (book.NumberOfCopies > 0) // Check if copies are available
                    {
                        // Decrease available copies
                        book.NumberOfCopies--;

                        if (book.NumberOfCopies == 0)
                        {
                            // Update availability status
                            book.IsAvailable = false;
                        }

                        await db.SaveChangesAsync();

                        // Record the borrow
                        await db.BorrowedBooks.AddAsync(borrowedBooks);
                        await db.SaveChangesAsync();
                    }
                    else
                    {
                        throw new Exception("No copies available for borrowing.");
                    }
                }
                else
                {
                    throw new Exception("Book not found.");
                }
            }
            catch (Exception)
            {
                // Rollback the decrement if an exception occurs
                await UpdateBookCopies(borrowedBooks.BookId, 1); // Increment the copies
                throw; // Rethrow the exception
            }
        }

        /// <summary>
        /// Checks if a user has borrowed a specific book.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <param name="bookId">The ID of the book.</param>
        /// <returns>True if the user has borrowed the book; otherwise, false.</returns>
        public async Task<bool> UserHasBorrowedBook(Guid userId, Guid bookId)
        {
            // Check if there are any borrow records for the user and book
            return await db.BorrowedBooks.AnyAsync(b => b.UserId == userId && b.BookId == bookId);
        }

        /// <summary>
        /// Updates the number of copies of a book.
        /// </summary>
        /// <param name="bookId">The ID of the book.</param>
        /// <param name="count">The number of copies to be added or subtracted.</param>
        /// <returns>The updated number of copies.</returns>
        public async Task<int> UpdateBookCopies(Guid bookId, int count)
        {
            var book = await db.Book.FirstOrDefaultAsync(b => b.Id == bookId);
            if (book != null)
            {
                book.NumberOfCopies += count;
                await db.SaveChangesAsync();
                return book.NumberOfCopies;
            }
            return -1; // Indicate failure if book is not found
        }

        /// <summary>
        /// Retrieves all borrowed books along with their associated book and user information.
        /// </summary>
        /// <returns>A collection of borrowed books.</returns>
        public async Task<IEnumerable<BorrowedBooks>> GetAllBorrowBook()
        {
            return await db.BorrowedBooks.Include(bb => bb.Book).Include(bb => bb.User).ToListAsync();
        }

        /// <summary>
        /// Saves changes to the database.
        /// </summary>
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }
    }

}

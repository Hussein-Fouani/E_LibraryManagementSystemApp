using AutoMapper;
using Azure;
using E_LibraryApi.Models.APIResponse;
using E_LibraryApi.Repository.IRepository;
using E_LibraryManagementSystem.Db;
using ELibrary.Domain.Models;
using ELibrary.Domain.NewFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using static System.Reflection.Metadata.BlobBuilder;

namespace E_LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly E_LibDb db;
        private readonly IMapper mapper;
        private readonly IBookRepository bookRepository;
        private readonly ISignInRepository signInRepository;
        private readonly IBorrowBook borrowBook;
        protected ApiReponse apireponse;

        public BookController(E_LibDb db, IMapper mapper, IBookRepository bookRepository, ISignInRepository signInRepository, IBorrowBook borrowBook)
        {
            this.db = db;
            this.mapper = mapper;
            this.bookRepository = bookRepository;
            this.signInRepository = signInRepository;
            this.borrowBook = borrowBook;
            apireponse = new ApiReponse();
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiReponse>> CreateBook([FromBody] BookDto book)
        {
            try
            {
                if (book == null)
                {

                    apireponse.Result = book;
                    apireponse.StatusCode = HttpStatusCode.Unauthorized;
                    apireponse.ErrorMessages.Add("Couldn't Add Book");
                    return BadRequest("Couldn't Add Book");
                }

                if (await bookRepository.BookExists(book.BookName))
                {
                    ModelState.AddModelError("BookName", "Book Name already exists");
                    apireponse.Result = book;
                    apireponse.StatusCode = HttpStatusCode.Unauthorized;
                    apireponse.ErrorMessages.Add("BookName already exists");
                    return BadRequest("Book Name already exists");

                }
                if (ModelState.IsValid)
                {
                    var books = mapper.Map<Book>(book);
                    await bookRepository.CreateBook(books);
                    apireponse.Result = books;
                    apireponse.StatusCode = HttpStatusCode.Created;
                    return CreatedAtAction("CreateBook", new { Id = books.Id }, apireponse);
                }
                return BadRequest(new { error = "Invalid model state" });
            }
            catch (Exception)
            {

                apireponse.IsSuccess = false;
                apireponse.StatusCode = HttpStatusCode.BadRequest;
                return apireponse;
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult<ApiReponse>> DeleteBook(Guid Id)
        {
            try
            {
                var book = await bookRepository.GetBook(b => b.Id == Id);

                if (book == null)
                {
                    return NotFound();
                }
                await bookRepository.DeleteBook(book);
                apireponse.StatusCode = HttpStatusCode.NoContent;
                apireponse.IsSuccess = true;
                return Ok(apireponse);
            }
            catch (Exception)
            {

                apireponse.IsSuccess = false;
                apireponse.StatusCode = HttpStatusCode.NotFound;
                return apireponse;
            }
        }
        [HttpPut]
        [Route("{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiReponse>> UpdateBook([FromBody] BookDto bookdto, [FromRoute] Guid Id)
        {
            try
            {
                if (bookdto == null)
                {
                    return BadRequest();
                }
                if (await bookRepository.GetBook(b => b.Id == Id) == null)
                {
                    return NotFound("Book Not Found");
                }
                var book = await bookRepository.GetBook(b => b.Id == Id);
                book.BookName = bookdto.BookName;
                book.BookAuthor = bookdto.BookAuthor;
                book.BookPublication = bookdto.BookPublication;
                book.BookPrice = bookdto.BookPrice;
                book.IsAvailable = bookdto.IsAvailable;
                book.Genre = bookdto.Genre;

                await bookRepository.UpdateBook(book);

                apireponse.StatusCode = HttpStatusCode.NoContent;
                apireponse.IsSuccess = true;

                return Ok(apireponse);
            }
            catch (Exception)
            {

                apireponse.IsSuccess = false;
                apireponse.StatusCode = HttpStatusCode.NotFound;
                return apireponse;
            }
        }
        [HttpGet("all", Name = "GetAllBooks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                var books = await bookRepository.GetAllBooks();
                if (books == null || books.Count == 0)
                {
                    return NotFound();
                }

                var response = mapper.Map<List<BookDto>>(books);

                return Ok(response);

            }
            catch (Exception)
            {

                return BadRequest("Books Not Found");
            }
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<BookDto>>> SearchBook([FromQuery] string query)
        {
            try
            {
                if (string.IsNullOrEmpty(query))
                {
                    return BadRequest("Search query is required.");
                }

                var allBooks = await bookRepository.GetAllBooks();
                var filteredResults = allBooks
     .Where(book =>
         book.BookName.ToUpper().Equals(query.ToUpper()) ||
         book.BookAuthor.ToUpper().Equals(query.ToUpper()) ||
         book.Genre.ToUpper().Equals(query.ToUpper()) ||
         book.ISBN.ToUpper().Equals(query.ToUpper()) ||
         book.Language.ToUpper().Equals(query.ToUpper()) ||
         book.BookPublication.ToUpper().Equals(query.ToUpper()) 
         
     )
     .Select(book => new BookDto
     {
         Id = book.Id,
         BookName = book.BookName,
         BookAuthor = book.BookAuthor,
         Genre = book.Genre,
         BookPublication = book.BookPublication,
         ISBN = book.ISBN,
         Language = book.Language,
         BookPrice = book.BookPrice,
        
     });
      


              

                return Ok(filteredResults);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }


        [HttpPost("addborrowedbook")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BorrowedBookInfo>> AddBorrowedBook(string username, string BookName)
        {
            try
            {
                // Check if the user exists
                var user = await signInRepository.GetUser(username);
                if (user == null)
                {
                    return NotFound("User not found.");
                }

                // Check if the book exists
                var book = await bookRepository.GetBook(i => i.BookName == BookName);
                if (book == null)
                {
                    return NotFound("Book not found.");
                }

                // Check if the book is available for borrowing
                if (!book.IsAvailable)
                {
                    apireponse.ErrorMessages.Add("Book is not available for borrowing.");
                    apireponse.IsSuccess = false;
                    apireponse.StatusCode = HttpStatusCode.NotAcceptable;
                    return BadRequest("not available");
                }
                // Update book availability status
                await bookRepository.UpdateBookAvailability(BookName, false);

                // Record the book borrowing in the database
                var borrowRecord = new BorrowedBooks
                {
                    UserId = user.UserId,
                    BookId = book.Id,
                    BorrowDate = DateTime.Now.Date.ToShortDateString(),
                    ReturnDate = DateTime.UtcNow.Date.AddDays(14).ToShortDateString()
                };
                await borrowBook.CreateBorrowBook(borrowRecord);
                // Return relevant information about the borrowed book
                var borrowedBookInfo = new BorrowedBookInfo
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    BookId = book.Id,
                    BookName = book.BookName,
                    Author = book.BookAuthor,
                    Genre = book.Genre,
                    BookPublication = book.BookPublication,
                    ISBN = book.ISBN,
                    Language = book.Language,
                    BookPrice = book.BookPrice,
                    BorrowDate = borrowRecord.BorrowDate,
                    ReturnDate = borrowRecord.ReturnDate
                };

                apireponse.Result = borrowedBookInfo;
                apireponse.StatusCode = HttpStatusCode.Created;
                apireponse.IsSuccess = true;
                return CreatedAtAction(nameof(GetUserBorrowedBooks), new { username = user.UserName }, borrowedBookInfo);
            }
            catch (Exception)
            {
                apireponse.IsSuccess = false;
                apireponse.StatusCode = HttpStatusCode.InternalServerError;
                return BadRequest("Can't borrow");
            }
        }
        [HttpGet("{username}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<BorrowedBookInfo>>> GetUserBorrowedBooks(string username)
        {
            try
            {
                var borrowedBooks = await borrowBook.GetAllBorrowBook();
                // Retrieve user's borrowed books with related book details
                var userBorrowedBooks = borrowedBooks
                .Where(bb => bb.User.UserName == username)
                .Select(bb => new BorrowedBookInfo
                {
                    UserId = bb.UserId,
                    BookId = bb.BookId,
                    BookName = bb.Book.BookName,
                    Author = bb.Book.BookAuthor,
                    Genre = bb.Book.Genre,
                    BookPublication = bb.Book.BookPublication,
                    BookPrice = bb.Book.BookPrice,
                    BorrowDate = bb.BorrowDate,
                    ISBN = bb.Book.ISBN,
                    Language = bb.Book.Language,
                    UserName = bb.User.UserName,
                    ReturnDate = bb.ReturnDate
                })
                .ToList();

                // Return the result
                return Ok(userBorrowedBooks);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }


    }
}

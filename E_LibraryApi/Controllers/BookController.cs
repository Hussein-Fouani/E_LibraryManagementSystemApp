using AutoMapper;
using E_LibraryApi.Models.APIResponse;
using E_LibraryApi.Repository.IRepository;
using ELibrary.Domain.Models;
using ELibrary.Domain.NewFolder;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace E_LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> logger;
        private readonly IMapper mapper;
        private readonly IBookRepository bookRepository;
        private readonly ISignInRepository signInRepository;
        private readonly IBorrowBook borrowBook;
        protected ApiReponse apiResponse;

        // Constructor for BookController
        public BookController(ILogger<BookController> logger, IMapper mapper, IBookRepository bookRepository, ISignInRepository signInRepository, IBorrowBook borrowBook)
        {
            this.logger = logger;
            this.mapper = mapper;
            this.bookRepository = bookRepository;
            this.signInRepository = signInRepository;
            this.borrowBook = borrowBook;
            apiResponse = new ApiReponse();
        }


        /// <summary>
        /// Endpoint for creating a new book.
        /// </summary>
        /// <param name="book">The book data to be created.</param>
        /// <returns>Created book data.</returns>
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
                    // Handle the case where the provided book is null
                    apiResponse.Result = book;
                    apiResponse.StatusCode = HttpStatusCode.Unauthorized;
                    apiResponse.ErrorMessages.Add("Couldn't Add Book");
                    logger.LogError("Book is null");
                    return BadRequest("Couldn't Add Book");
                }

                var checkbook= await bookRepository.BookExists(book.BookName);
                var checks = await bookRepository.GetBook(b => b.BookName == book.BookName);
                if ( checkbook && book.NumberOfCopies>0)
                {
                    // Handle the case where the book with the same name already exists
                    ModelState.AddModelError("BookName", "Book Copies Increased");
                   checks.NumberOfCopies += book.NumberOfCopies;
                    await bookRepository.Save();
                    apiResponse.Result = book;
                    apiResponse.StatusCode = HttpStatusCode.Created;
                    logger.LogError("Book Copies Increased");
                    return Ok("Book Copies Increased");
                    
                    

                }
                if (ModelState.IsValid)
                {
                    // Map the BookDto to Book and create the book
                    var books = mapper.Map<Book>(book);
                    await bookRepository.CreateBook(books);
                    apiResponse.Result = books;
                    apiResponse.StatusCode = HttpStatusCode.Created;
                    logger.LogError("Created SuccessFully");
                    return CreatedAtAction("CreateBook", new { Id = books.Id }, apiResponse);
                }
                logger.LogError("Invalid Data provided");
                return BadRequest(new { error = "Invalid model state" });
            }
            catch (Exception ex)
            {

                logger.LogError(ex.Message);
                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessages.Add(ex.Message);
                apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return apiResponse;
            }
        }

        /// <summary>
        /// Deletes a book by its ID.
        /// </summary>
        /// <param name="Id">The ID of the book to delete.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult<ApiReponse>> DeleteBook(Guid Id)
        {
            try
            {
                // Attempt to retrieve the book by its Id
                var book = await bookRepository.GetBook(b => b.Id == Id);

                if (book == null)
                {
                    // Handle the case where the book is not found
                    logger.LogError("Book Not Found");
                    return NotFound("Book Not Found");
                    
                }
                // Delete the book and return a success response
                await bookRepository.DeleteBook(book);
                apiResponse.StatusCode = HttpStatusCode.NoContent;
                apiResponse.IsSuccess = true;
                return Ok(apiResponse);
            }
            catch (Exception ex)
            {

                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessages.Add(ex.Message);
                apiResponse.StatusCode = HttpStatusCode.NotFound;
                return apiResponse;
            }
        }

        /// <summary>
        /// Updates an existing book with the provided data.
        /// </summary>
        /// <param name="bookDto">The updated book data.</param>
        /// <param name="Id">The ID of the book to update.</param>
        /// <returns>Action result indicating success or failure.</returns>
        [HttpPut]
        [Route("{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiReponse>> UpdateBook([FromBody] BookDto bookDto, [FromRoute] Guid Id)
        {
            try
            {
                if (bookDto == null)
                {
                    // Handle the case where the provided book is null
                    logger.LogError("Book Provided is Null");
                    return BadRequest();
                }
                // Check if the book with the given Id exists
                if (await bookRepository.GetBook(b => b.Id == Id) == null)
                {
                    logger.LogError("Book Not Found");
                    return NotFound("Book Not Found");
                }
                // Retrieve the book, update its properties, and save changes
                var book = await bookRepository.GetBook(b => b.Id == Id);
                book.BookName = bookDto.BookName;
                book.BookAuthor = bookDto.BookAuthor;
                book.BookPublication = bookDto.BookPublication;
                book.BookPrice = bookDto.BookPrice;
                book.ISBN = bookDto.ISBN;
                book.Language = bookDto.Language;
                book.NumberOfCopies = bookDto.NumberOfCopies;
                book.IsAvailable = bookDto.IsAvailable;
                book.Genre = bookDto.Genre;

                await bookRepository.UpdateBook(book);

                apiResponse.StatusCode = HttpStatusCode.NoContent;
                apiResponse.IsSuccess = true;

                return Ok(apiResponse);
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                apiResponse.IsSuccess = false;
                apiResponse.ErrorMessages.Add(ex.Message);
                apiResponse.StatusCode = HttpStatusCode.NotFound;
                return apiResponse;
            }
        }

        /// <summary>
        /// Retrieves all books from the library.
        /// </summary>
        /// <returns>Action result containing a list of books.</returns>
        [HttpGet("all", Name = "GetAllBooks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                // Retrieve all books from the repository
                var books = await bookRepository.GetAllBooks();
                if (books == null || books.Count == 0)
                {
                    // Handle the case where no books are found
                    logger.LogError("Book Not Found");
                    return NotFound();
                   
                }
                // Map the list of books to a list of BookDto
                var response = mapper.Map<List<BookDto>>(books);

                return Ok(response);

            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                logger.LogError(ex.Message);
                return BadRequest("Books Not Found");
            }
        }

        /// <summary>
        /// Searches for books based on the provided query.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>Action result containing a list of matched books.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<BookDto>>> SearchBook([FromQuery] string query)
        {
            try
            {
                // Handle the case where the search query is not supplied
                if (string.IsNullOrEmpty(query))
                {
                    logger.LogError("Search query Not Supplied");
                    return BadRequest("Search query is required.");
                }
                // Retrieve all books from the repository
                var allBooks = await bookRepository.GetAllBooks();
                // Filter books based on the search query
                var filteredResults = allBooks
                         .Where(book =>
                             book.BookName.ToUpper().Contains(query.ToUpper()) ||
                             book.BookAuthor.ToUpper().Contains(query.ToUpper()) ||
                             book.Genre.ToUpper().Contains(query.ToUpper()) ||
                             book.ISBN.ToUpper().Contains(query.ToUpper()) ||
                             book.Language.ToUpper().Contains(query.ToUpper()) ||
                             book.BookPublication.ToUpper().Contains(query.ToUpper()) 
         
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
                             NumberOfCopies=book.NumberOfCopies,
                             BookPrice = book.BookPrice,
                             IsAvailable=book.IsAvailable,
                         });

                return Ok(filteredResults);
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                logger.LogError(ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// Adds a new borrowed book record for a user.
        /// </summary>
        /// <param name="username">The username of the borrower.</param>
        /// <param name="BookName">The name of the book to be borrowed.</param>
        /// <returns>Action result indicating success or failure.</returns>
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
                if ( await borrowBook.UserHasBorrowedBook(user.UserId, book.Id))
                {
                    return BadRequest("User has already borrowed this book.");
                }

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
                    ReturnDate = borrowRecord.ReturnDate,
                   
                };

                return CreatedAtAction(nameof(GetUserBorrowedBooks), new { username = user.UserName }, borrowedBookInfo);
            }
            catch (Exception ex)
            {
                // Handle unexpected exceptions
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Retrieves all books borrowed by a specific user.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <returns>Action result containing a list of borrowed books.</returns>

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

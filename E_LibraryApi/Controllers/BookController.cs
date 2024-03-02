using AutoMapper;
using E_LibraryApi.Mapper;
using E_LibraryApi.Models;
using E_LibraryApi.Models.APIResponse;
using E_LibraryApi.Models.Dto;
using E_LibraryApi.Repository.IRepository;
using E_LibraryManagementSystem.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Net;

namespace E_LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly E_LibDb db;
        private readonly IMapper mapper;
        private readonly IBookRepository bookRepository;
        protected ApiReponse apireponse;

        public BookController(E_LibDb db, IMapper mapper, IBookRepository bookRepository)
        {
            this.db = db;
            this.mapper = mapper;
            this.bookRepository = bookRepository;
            apireponse = new ApiReponse();
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiReponse>> CreateBook([FromBody] BookDto book)
        {
            try
            {
                if (book == null)
                {
                    return BadRequest("Couldn't Add Book");
                }

                if (await bookRepository.BookExists(book.BookName))
                {
                    ModelState.AddModelError("BookName", "Book Name already exists");
                    return BadRequest("Book Name already exists");
                }
                if (ModelState.IsValid)
                {
                    var books = mapper.Map<BookModel>(book);
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
        [HttpDelete("{Id:guid}")]
        public async Task<ActionResult<ApiReponse>> DeleteBook(Guid Id)
        {
            try
            {
                var book = await bookRepository.GetBook(b => b.Id==Id);

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
        [HttpPut("{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiReponse>> UpdateBook([FromBody] BookDto bookdto, Guid Id)
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
                book.BookPurhcaseDate = bookdto.BookPurhcaseDate;
                book.BookQuantity = bookdto.BookQuantity;

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
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiReponse>> GetAllBooks()
        {
            try
            {                
                    var books = await bookRepository.GetAllBooks();
                    if (books == null || books.Count == 0)
                    {
                        return NotFound();
                    }

                    apireponse.Result = mapper.Map<List<BookDto>>(books);
                    apireponse.StatusCode = HttpStatusCode.OK;
                    return Ok(apireponse);
                
            }
            catch (Exception)
            {

                apireponse.IsSuccess = false;
                apireponse.StatusCode = HttpStatusCode.NotFound;
                return apireponse;
            }
        }


        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiReponse>> SearchBook([FromQuery] string bookName = null, [FromQuery] string authorName = null)
        {
            try
            {
                Expression<Func<BookModel, bool>> filter = null;

                if (!string.IsNullOrEmpty(bookName))
                {
                    filter = b => b.BookName.ToLowerInvariant()==bookName.ToLowerInvariant();
                }
                else if (!string.IsNullOrEmpty(authorName))
                {
                    filter = b => b.BookAuthor.ToLowerInvariant() == authorName.ToLowerInvariant();
                }

                var books = await bookRepository.GetBook(filter);

                if (books == null)
                {
                    return NotFound();
                }

                apireponse.Result = mapper.Map<List<BookDto>>(books);
                apireponse.StatusCode = HttpStatusCode.OK;
                return Ok(apireponse);
            }
            catch (Exception)
            {
                apireponse.IsSuccess = false;
                apireponse.StatusCode = HttpStatusCode.NotFound;
                return apireponse;
            }
        }





    }
}

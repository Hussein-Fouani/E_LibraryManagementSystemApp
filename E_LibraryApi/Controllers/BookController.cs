using AutoMapper;
using E_LibraryApi.Mapper;
using E_LibraryApi.Models;
using E_LibraryApi.Models.APIResponse;
using E_LibraryApi.Models.Dto;
using E_LibraryApi.Repository.IRepository;
using E_LibraryManagementSystem.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
                apireponse.ErrorMessages.Add("Couldn't Add Book");
                apireponse.StatusCode = HttpStatusCode.BadRequest;
                return apireponse;
            }
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult<ApiReponse>> DeleteBook(BookDto bookname)
        {
            try
            {
                var book = await bookRepository.GetBook(b => b.BookName.ToLowerInvariant().Equals(bookname.BookName.ToLowerInvariant()));

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
        [HttpPut("{Id:Guid}")]
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
                var book = await bookRepository.GetBook(b => b.Id == Id, tracked: false);

                var mapped = mapper.Map<BookModel>(bookdto);
                await bookRepository.UpdateBook(mapped);

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
        [HttpGet(Name = "getallbooks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiReponse>> GetAllBooks(string name = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(name))
                {
                    var book = bookRepository.GetBook(b => b.BookName.ToLowerInvariant().Contains(name.ToLowerInvariant()));
                    if (book == null)
                    {
                        return NotFound();
                    }

                    apireponse.Result = mapper.Map<BookDto>(book);
                    apireponse.StatusCode = HttpStatusCode.OK;
                    return Ok(apireponse);
                }
                else
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
        public async Task<ActionResult<ApiReponse>> SearchBookByName([FromBody] BookDto book)
        {
            try
            {
                var books = await bookRepository.GetBook(b => b.BookName.ToLowerInvariant().Contains(book.BookName.ToLowerInvariant()));
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
        [HttpGet("{authorname:string}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiReponse>> SearchBookByAuthor([FromBody] BookDto bookDto)
        {
            try
            {
                if (bookDto == null)
                {
                    return BadRequest("Please Supply Data");
                }
                if (string.IsNullOrEmpty(bookDto.BookName))
                {
                    return BadRequest("BookName is not Supplied");
                }
                var book = await bookRepository.GetBook(b => b.BookAuthor.ToLowerInvariant() == bookDto.BookAuthor.ToLowerInvariant());
                if (book == null)
                {
                    ModelState.AddModelError("", "Author Not Found");
                    return BadRequest(ModelState);
                }
                apireponse.Result = mapper.Map<BookModel>(book);
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

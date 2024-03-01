using AutoMapper;
using E_LibraryApi.Mapper;
using E_LibraryApi.Models;
using E_LibraryApi.Models.Dto;
using E_LibraryApi.Repository.IRepository;
using E_LibraryManagementSystem.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly E_LibDb db;
        private readonly IMapper mapper;
        private readonly IBookRepository bookRepository;

        public BookController(E_LibDb db,IMapper mapper,IBookRepository bookRepository)
        {
            this.db = db;
            this.mapper = mapper;
            this.bookRepository = bookRepository;
        }
        [HttpPost]
        public  async Task<IActionResult> CreateBook([FromBody] BookDto book)
        {
            if(book == null)
            {
                return BadRequest("Couldn't Add Book"); 
            }

            if ( await bookRepository.BookExists(book.BookName))
            {
                ModelState.AddModelError("BookName", "Book Name already exists");
                return BadRequest("Book Name already exists");
            }
            if (ModelState.IsValid)
            {
                var books = mapper.Map<BookModel>(book);
                await bookRepository.CreateBook(books);
                return CreatedAtAction("CreateBook", new {Id=Guid.NewGuid()},books);
            }
            return BadRequest(new { error = "Invalid model state" });
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteBook(BookDto bookname)
        {
            var book = await bookRepository.GetBook(b => b.BookName.ToLowerInvariant().Equals(bookname.BookName.ToLowerInvariant()));
            if (book == null)
            {
                return NotFound();
            }
            await bookRepository.DeleteBook(book);
            return NoContent();
        }
        [HttpPut("{Id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateBook([FromBody]BookDto bookdto,Guid Id)
        {
            if (bookdto == null)
            {
                return BadRequest();
            }
            if(await bookRepository.GetBook(b=>b.Id == Id) == null)
            {
                return NotFound("Book Not Found");
            }
            var book = await bookRepository.GetBook(b => b.Id == Id,tracked:false);
            
            var mapped =  mapper.Map<BookModel>(bookdto);

            await bookRepository.UpdateBook(mapped);
            return Ok("Updated SuccessFully");
        }
        [HttpGet(Name ="getallbooks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllBooks(string name=null)
        {
            if(!string.IsNullOrEmpty(name))
            {
                var book = bookRepository.GetBook(b => b.BookName.ToLowerInvariant().Contains(name.ToLowerInvariant()));
                if (book == null )
                {
                    return NotFound();
                }
                var mapped = mapper.Map<BookDto>(book);
                return Ok(mapped);
            }
            else
            {
                var books = await bookRepository.GetAllBooks();
                if (books == null || books.Count== 0)
                {
                    return NotFound();
                }
                var mapped = mapper.Map<List<BookDto>>(books);
                return Ok(mapped);
            }
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchBookByName([FromBody] BookDto book)
        {
            var books = await bookRepository.GetBook(b=>b.BookName.ToLowerInvariant().Contains(book.BookName.ToLowerInvariant()));
            if (books == null)
            {
                return NotFound();
            }
            var mapped = mapper.Map<List<BookDto>>(books);
            return Ok(mapped);
        }
        [HttpGet("{authorname:string}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SearchBookByAuthor([FromBody] BookDto bookDto)
        {
            if(bookDto == null)
            {
                return BadRequest("Please Supply Data");
            }
            if (string.IsNullOrEmpty(bookDto.BookName))
            {
                return BadRequest("BookName is not Supplied");
            }
            var book = await bookRepository.GetBook(b=>b.BookAuthor.ToLowerInvariant() ==bookDto.BookAuthor.ToLowerInvariant());
            if(book == null)
            {
                ModelState.AddModelError("", "Author Not Found");
                return BadRequest(ModelState);
            }
            return Ok(mapper.Map<BookModel>(book));
        }

        

    }
}

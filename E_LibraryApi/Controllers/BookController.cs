using E_LibraryApi.Models;
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

        
        public BookController(E_LibDb db)
        {
            this.db = db;
        }
        [HttpPost]
        public  async Task<IActionResult> AddBook([FromBody] BookModel book)
        {
            if(book == null)
            {
                return BadRequest("Couldn't Add Book"); }
            
            if (ModelState.IsValid)
            {
                await db.Book.AddAsync(book);
                await db.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
    }
}

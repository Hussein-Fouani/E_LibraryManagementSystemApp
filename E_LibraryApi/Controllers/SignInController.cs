using E_LibraryApi.Models.Dto;
using E_LibraryManagementSystem.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly E_LibDb db;

        public SignInController(E_LibDb db)
        {
            this.db = db;
        }
        [HttpGet("Id:Guid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SignInUser(SignInDto signindto)
        {
            return null;
        }
    }
}

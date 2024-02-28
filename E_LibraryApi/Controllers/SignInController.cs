using E_LibraryApi.Models.Dto;
using E_LibraryApi.Repository;
using E_LibraryManagementSystem.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace E_LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly E_LibDb db;
        private readonly SignInRepository signInRepository;

        public SignInController(E_LibDb db,SignInRepository signInRepository)
        {
            this.db = db;
            this.signInRepository = signInRepository;
        }
        [HttpGet("username:string")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SignInUser(SignInDto signindto)
        {
            if(signindto == null)
            {
                return BadRequest("Please Supply UserInfo");
            }
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(string.IsNullOrEmpty(signindto.UserName) || string.IsNullOrEmpty(signindto.Password))
            {
                return BadRequest("Username and Password must be at least 4 characters");
            }

        }
    }
}

using AutoMapper;
using E_LibraryApi.Repository;
using E_LibraryApi.Repository.IRepository;
using E_LibraryManagementSystem.Db;
using ELibrary.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly E_LibDb db;
        private readonly ISignInRepository signInRepository;
        private readonly IMapper mapper;

        public SignInController(E_LibDb db,ISignInRepository signInRepository,IMapper mapper)
        {
            this.db = db;
            this.signInRepository = signInRepository;
            this.mapper = mapper;
            
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SignInUser([FromQuery]string username,string password)
        {
           

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Please Supply UserInfo");
            }

            var user = await signInRepository.GetUser(username);

            if (user==null)
            {
                return NotFound("User Not Found");
            }
            if (user.Password != password)
            {
                return BadRequest("Invalid Password");
            }
            try
            {
                UserRL signedInUser = await signInRepository.SignInAsync(user.UserName, user.Password);
                return Ok(signedInUser);
            }
            catch (Exception )
            {
                return BadRequest($"Invalid Username or Password");
            }
        }

    }
}

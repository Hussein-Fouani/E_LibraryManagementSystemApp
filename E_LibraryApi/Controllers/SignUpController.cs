using E_LibraryApi.Repository;
using E_LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog.Core;

namespace E_LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly SignUpRepository signUp;
        private readonly ILogger<SignUpController> logger;

        public SignUpController(SignUpRepository signUp,ILogger<SignUpController> logger)
        {
            this.signUp = signUp;
            this.logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel signUpModel)
        {
            if (ModelState.IsValid)
            {
                if (await signUp.CheckIfUserExists(signUpModel.UserName))
                {
                  await signUp.SignUpAsync(signUpModel);
                }
                else
                {
                    logger.LogWarning("User with username {Username} already exists.\", signUpModel.UserName",signUpModel.UserName);
                    return BadRequest("User Already Exists");
                }
            }
            logger.LogError( "An error occurred while processing the request.");
              return BadRequest("Something Went Wrong"); 
        }

        [HttpGet("{Id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            return Ok("Hello World");
        }
    }
}

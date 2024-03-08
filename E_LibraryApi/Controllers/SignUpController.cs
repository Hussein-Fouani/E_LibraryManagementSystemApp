using E_LibraryApi.Models.APIResponse;
using E_LibraryApi.Repository;
using E_LibraryApi.Repository.IRepository;
using ELibrary.Domain.Models;
using ELibrary.Domain.NewFolder;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace E_LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly ISignUpRepository signUp;
        protected ApiReponse apiResponse;
        private readonly ILogger<SignUpController> logger;

        public SignUpController(ISignUpRepository signUp, ILogger<SignUpController> logger)
        {
            this.signUp = signUp;
            this.logger = logger;
            apiResponse=new ApiReponse();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<bool>> SignUp([FromBody] SignUPDto user)
        {
            try
            {
                if (await signUp.CheckIfUserExists(user.Username))
                {
                    logger.LogWarning("User with username {Username} already exists.", user.Username);
                    apiResponse.IsSuccess = false;
                    apiResponse.StatusCode = HttpStatusCode.Forbidden;
                    apiResponse.ErrorMessages.Add("User already exists");
                    return BadRequest(apiResponse);
                }

             

                // Check if password matches the confirmation password
                if (user.Password != user.ConfirmPassword)
                {
                    apiResponse.IsSuccess = false;
                    apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    apiResponse.ErrorMessages.Add("Password and confirmation password do not match.");
                    return BadRequest(apiResponse); 
                }

                // Hash the password
               /* IPasswordHasher hasher = new PasswordHasher();
                string hashedPassword = hasher.HashPassword(user.Password);*/

                // Create a new User model
                SignUp model = new SignUp()
                {
                    Username = user.Username,
                    Password = user.Password,
                    Email=user.Email,
                    ConfirmPassword = user.ConfirmPassword
                };
                // Check if the user already exists
                

                // Perform user registration
                await signUp.SignUpAsync(model);

                // Set success flag to true
                return Ok(true);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while processing the request.");
                apiResponse.IsSuccess = false;
                apiResponse.StatusCode = HttpStatusCode.BadRequest;
                apiResponse.ErrorMessages.Add(ex.Message);
                return BadRequest(apiResponse);
            }
        }


    }
}

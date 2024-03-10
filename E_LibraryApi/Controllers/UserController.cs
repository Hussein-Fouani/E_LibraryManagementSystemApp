using E_LibraryApi.Models.APIResponse;
using E_LibraryApi.Repository.IRepository;
using ELibrary.Domain.Models;
using ELibrary.Domain.NewFolder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace E_LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private ApiReponse apiresponse;

        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
            apiresponse = new ApiReponse();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto loginRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var response = await userRepository.Login(loginRequestDto);
                if (response == null || string.IsNullOrEmpty(response.Token))
                {
                    apiresponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    apiresponse.ErrorMessages.Add("Invalid Username or Password");
                    apiresponse.IsSuccess = false;
                    return BadRequest(apiresponse);
                }
                apiresponse.StatusCode = System.Net.HttpStatusCode.OK;
                apiresponse.IsSuccess = true;
                apiresponse.Result = response;
                return Ok(apiresponse);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                apiresponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                apiresponse.IsSuccess = false;
                apiresponse.ErrorMessages.Add(ex.Message);
                return BadRequest("apiresponse");
            }
            catch (Exception)
            {
                apiresponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                apiresponse.IsSuccess = false;
                apiresponse.ErrorMessages.Add("Something Went Wrong");
                return BadRequest("apiresponse");
            }
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Registration([FromBody]RegistrationDto RequestDto)
        {
            try
            {
                bool response =  userRepository.IsUniqueUser(RequestDto.UserName);
                if (!response)
                {
                    apiresponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    apiresponse.ErrorMessages.Add("Username Already Exists");
                    apiresponse.IsSuccess = false;
                    return BadRequest(apiresponse);
                }
                User user = await userRepository.Register(RequestDto);
                if (user == null)
                {
                    apiresponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    apiresponse.ErrorMessages.Add("Error While Registering");
                    apiresponse.IsSuccess = false;
                    return BadRequest(apiresponse);
                }
                apiresponse.StatusCode = System.Net.HttpStatusCode.OK;
                apiresponse.IsSuccess = true;
                return Ok(apiresponse);
            }
            catch (Exception ex)
            {

                apiresponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                apiresponse.ErrorMessages.Add(ex.Message);
                apiresponse.IsSuccess = false;
                return BadRequest(apiresponse);
            }
        }
    }
}

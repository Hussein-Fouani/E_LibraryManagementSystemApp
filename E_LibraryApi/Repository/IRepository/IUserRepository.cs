using ELibrary.Domain.Models;
using ELibrary.Domain.NewFolder;

namespace E_LibraryApi.Repository.IRepository
{
    public interface IUserRepository
    {
       bool IsUniqueUser(string username);
        Task<LoginResponseDto> Login(LoginRequestDto model);
        Task<User> Register(RegistrationDto model);
    }
}

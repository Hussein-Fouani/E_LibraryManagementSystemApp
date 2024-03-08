using ELibrary.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_LibraryApi.Repository.IRepository
{
    public interface ISignUpRepository
    {
        Task SignUpAsync(SignUp user);
        Task Save();
        Task<bool> CheckIfUserExists(string Username);
        Task<bool> CheckIfEmailExists(string Email);
        Task<SignUp> GetUser(string Username);
    }
}

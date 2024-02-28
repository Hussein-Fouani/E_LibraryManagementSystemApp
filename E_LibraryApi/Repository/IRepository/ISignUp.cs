using E_LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_LibraryApi.Repository.IRepository
{
    public interface ISignUp
    {
        Task SignUpAsync(SignUpModel signUpModel);
        Task Save();
        Task<bool> CheckIfUserExists(string Username);
    }
}

using ELibrary.Domain.Models;

namespace E_LibraryApi.Repository.IRepository
{
    public interface ISignInRepository
    {
        
        Task<UserRL> GetUser(string userName);
        Task<UserRL> SignInAsync(string username,string password );
    }
}

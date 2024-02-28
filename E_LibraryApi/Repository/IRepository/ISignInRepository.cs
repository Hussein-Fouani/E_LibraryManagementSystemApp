using E_LibraryManagementSystem.Models;

namespace E_LibraryApi.Repository.IRepository
{
    public interface ISignInRepository
    {
        
        Task<bool> AuthenticateAsync(string userName, string password);
        Task SignInAsync(SignInModel model);
    }
}

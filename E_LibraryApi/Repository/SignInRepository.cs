using E_LibraryApi.Repository.IRepository;
using E_LibraryManagementSystem.Models;

namespace E_LibraryApi.Repository
{
    public class SignInRepository:ISignInRepository
    {
        public Task<bool> AuthenticateAsync(string userName, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task SignInAsync(SignInModel model)
        {
            throw new System.NotImplementedException();
        }
    }
   
}
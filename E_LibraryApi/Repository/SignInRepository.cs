using E_LibraryApi.Repository.IRepository;
using E_LibraryManagementSystem.Db;
using E_LibraryManagementSystem.Models;

namespace E_LibraryApi.Repository
{
    public class SignInRepository:ISignInRepository
    {
        private readonly E_LibDb db;

        public SignInRepository(E_LibDb db)
        {
            this.db = db;
        }
        public Task<bool> AuthenticateAsync(string userName, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task SignInAsync(SignInModel model)
        {
            
        }
    }
   
}
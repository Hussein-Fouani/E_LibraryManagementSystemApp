using E_LibraryApi.Controllers;
using E_LibraryApi.Repository.IRepository;
using E_LibraryManagementSystem.Db;
using E_LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace E_LibraryApi.Repository
{
    public class SignInRepository:ISignInRepository
    {
        private readonly E_LibDb db;
        private readonly ILogger<SignInRepository> logger;

        public SignInRepository(E_LibDb db, ILogger<SignInRepository> logger)
        {
            this.db = db;
            
            this.logger = logger;
        }
        public Task<bool> AuthenticateAsync(string userName, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SignInModel>  SignInAsync(SignInModel model)
        {
          return  await db.SignIn.FirstOrDefaultAsync(m=>m.UserName==model.UserName);  
        }
    }
   
}
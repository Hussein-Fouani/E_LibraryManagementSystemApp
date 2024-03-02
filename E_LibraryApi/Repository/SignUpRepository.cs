using E_LibraryApi.Repository.IRepository;
using E_LibraryManagementSystem.Db;
using E_LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_LibraryApi.Repository
{
    public class SignUpRepository :ISignUpRepository
    {
        private readonly E_LibDb db;

        public SignUpRepository(E_LibDb db)
        {
            this.db = db;
        }

        public async Task<bool> CheckIfUserExists(string username)
        {
           var user = await db.SignIn.FirstOrDefaultAsync(x => x.UserName == username);
            
            return user!=null;
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

        public async Task SignUpAsync(SignUpModel signUpModel)
        {
            await db.Signup.AddAsync(signUpModel);
            await Save();

        }
    }
}

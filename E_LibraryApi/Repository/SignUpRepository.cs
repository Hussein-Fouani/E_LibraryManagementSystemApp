using E_LibraryApi.Repository.IRepository;
using E_LibraryManagementSystem.Db;
using ELibrary.Domain.Models;
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
            return await db.SignUP.AnyAsync(x => x.Username == username);
        }
        public async Task<bool> CheckIfEmailExists(string Email)
        {
            return await db.SignUP.AnyAsync(x => x.Email == Email);
        }
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

        public async Task SignUpAsync(SignUp user)
        {
            await db.SignUP.AddAsync(user);
            await Save();

        }

        public async Task<SignUp> GetUser(string Username)
        {
          return  await db.SignUP.FirstOrDefaultAsync(x => x.Username == Username);
        }
    }
}

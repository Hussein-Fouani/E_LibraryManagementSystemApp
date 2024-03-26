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
            if(await db.SignUP.FirstOrDefaultAsync(x => x.Username.ToLower() == username.ToLower()) == null)
            {
                return false;
            }
            return true;
        }
        public async Task<bool> CheckIfEmailExists(string Email)
        {
            if (await db.SignUP.FirstOrDefaultAsync(x => x.Email.ToLower() == Email.ToLower()) == null)
            {
                return false;
            }
            return true;
        }
        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

        public async Task SignUpAsync(SignUp user)
        {
            var userrole = new UsersRole()
            {
                Role = "User",
                UserName = user.Username
            };

            if (await db.UsersRole.FirstOrDefaultAsync(x => x.UserName.ToLower() == user.Username.ToLower()) == null)
            {
              
                await db.UsersRole.AddAsync(userrole);
                await db.SaveChangesAsync();
            }
            user.Role=userrole.Role;
           
            await db.SignUP.AddAsync(user);
            await Save();

        }
        public async Task<SignUp> GetUser(string Username)
        {
          return  await db.SignUP.FirstOrDefaultAsync(x => x.Username.ToLower() == Username.ToLower());
        }
    }
}

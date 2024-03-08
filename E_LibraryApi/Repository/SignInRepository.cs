using E_LibraryApi.Repository.IRepository;
using E_LibraryManagementSystem.Db;
using ELibrary.Domain.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace E_LibraryApi.Repository
{
    public class SignInRepository: ISignInRepository
    {
        private readonly E_LibDb db;
        private readonly ILogger<SignInRepository> logger;
        private IPasswordHasher passwordHasher;

        public SignInRepository(E_LibDb db, ILogger<SignInRepository> logger, IPasswordHasher passwordHasher)
        {
            this.db = db;
            this.logger = logger;
            this.passwordHasher = passwordHasher;
        }



        public async Task<UserRL> GetUser(string userName)
        {
            try
            {
                var user = await db.SignUP.FirstOrDefaultAsync(m => m.Username == userName);

                if (user == null)
                {
                    return null;
                }

                var userRL = new UserRL
                {
                    UserId = user.Id,
                    UserName = user.Username,
                    Password = user.Password,
                    Email = user.Email
                };

                return userRL;
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Exception in GetUser: {ex}");
                return null;
            }
        }

        public async Task<UserRL>  SignInAsync(string username, string password)
        {
          var user=  await GetUser(username);
            
            if (user == null)
            {
                return null;
            }
         
           /* PasswordVerificationResult passwordVerificationResult = passwordHasher.VerifyHashedPassword(user.Password, password);*/
            if (user.Password != password)
            {
                return null;
            }
            if(db.User.FirstOrDefaultAsync(m=>m.UserName==username)!=null)
            {
                return user;
            }
            await db.Users.AddAsync(user);
            await db.SaveChangesAsync();
            return user;
        }

        
    }
   
}
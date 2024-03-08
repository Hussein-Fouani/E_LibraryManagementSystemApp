using E_LibraryApi.Repository.IRepository;
using E_LibraryManagementSystem.Db;
using ELibrary.Domain.Models;
using ELibrary.Domain.NewFolder;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_LibraryApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly E_LibDb db;
        string privateKey;
        public UserRepository(E_LibDb db, IConfiguration configuration)
        {
            this.db = db;
            privateKey = configuration.GetValue<string>("ApiSettings:Secret");
        }
        public  bool IsUniqueUser(string username)
        {
            var user =  db.User.FirstOrDefault(u => u.UserName == username);
            return user == null;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto model)
        {
            var user = await db.User.FirstOrDefaultAsync(d => d.UserName.ToLower() == model.UserName.ToLower() && d.Password == model.Password);
            if (user == null)
            {
                LoginResponseDto res = new LoginResponseDto()
                {
                    Token = "",
                    User = null
                };
                return res;
            }
            var token = new JwtSecurityTokenHandler();
            byte[] keybytes = Encoding.ASCII.GetBytes(privateKey);

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                       new Claim(ClaimTypes.Name,user.Id.ToString()),
                       new Claim(ClaimTypes.Role,  user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keybytes), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenhandler = token.CreateToken(TokenDescriptor);
            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                Token=token.WriteToken(tokenhandler),
                User = user
            };
            return loginResponseDto;
        }

        public async Task<User> Register(RegistrationDto model)
        {
            User user = new User()
            {
                UserName = model.UserName,
                Password = model.Password,
                Role = model.Role,
                Name = model.Name
            };
            await db.User.AddAsync(user);
           
            await db.SaveChangesAsync();
            user.Password = "";

            return user;
        }
    }
}

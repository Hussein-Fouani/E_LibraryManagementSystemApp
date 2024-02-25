using E_LibraryApi.Models;
using E_LibraryManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LibraryManagementSystem.Db
{
    public class E_LibDb:DbContext
    {
        public E_LibDb(DbContextOptions<E_LibDb> options):base(options)
        {
            
        }
        public DbSet<SignInModel> SignIn { get; set; }
        public DbSet<BookModel> Book { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SignInModel>().HasData(
                new SignInModel
                {
                    Id =Guid.Parse("e6cb49e3-6905-4800-a6ca-a9b645ab18ef"),
                    UserName = "Admin",
                    Password = "Admin"
                }
                );
        }
    }
}

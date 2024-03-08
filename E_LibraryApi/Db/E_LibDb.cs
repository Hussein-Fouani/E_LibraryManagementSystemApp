using ELibrary.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace E_LibraryManagementSystem.Db
{
    public class E_LibDb:DbContext
    {
        public E_LibDb(DbContextOptions<E_LibDb> options):base(options)
        {
            
        }
        public DbSet<Book> Book { get; set; }
        public DbSet<Student> Student { get; set;}
        public DbSet<User> User { get; set; }
        public DbSet<BorrowedBooks> BorrowedBooks { get; set; }
        public DbSet<UserRL> Users { get; set; }
        public DbSet<SignUp> SignUP { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            

            
            modelBuilder.Entity<BorrowedBooks>()
                .HasOne(bb => bb.Book)
                .WithMany(b => b.BorrowedBooks)
                .HasForeignKey(bb => bb.BookId)
                .OnDelete(DeleteBehavior.Cascade);

           
            modelBuilder.Entity<BorrowedBooks>()
                .HasOne(bb => bb.User)
                .WithMany(s => s.BorrowedBooks)
                .HasForeignKey(bb => bb.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}

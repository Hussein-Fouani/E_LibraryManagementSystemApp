using ELibrary.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.EF.DB
{
    public class ELibraryDbContext : DbContext
    {

        public DbSet<Book> Books { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BorrowedBooks> BorrowedBook { get; set; }

        public ELibraryDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            // Configure Book and BorrowedBooks relationship
            modelBuilder.Entity<BorrowedBooks>()
                .HasOne(bb => bb.Book)
                .WithMany(b => b.BorrowedBooks)
                .HasForeignKey(bb => bb.BookId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure Student and BorrowedBooks relationship
            modelBuilder.Entity<BorrowedBooks>()
                .HasOne(bb => bb.Student)
                .WithMany(s => s.BorrowedBooks)
                .HasForeignKey(bb => bb.StudentId)
                .OnDelete(DeleteBehavior.Restrict);


            base.OnModelCreating(modelBuilder);
        }
    }


}

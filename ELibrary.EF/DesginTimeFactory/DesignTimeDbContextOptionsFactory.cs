using ELibrary.EF.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.EF.DesginTimeFactory
{
    public class DesignTimeDbContextOptionsFactory : IDesignTimeDbContextFactory<ELibraryDbContext>
    {
        public ELibraryDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<ELibraryDbContext>();
               options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ElibraryDb;Integrated Security=True");
            return new ELibraryDbContext(options.Options);
                
        }
    }
}

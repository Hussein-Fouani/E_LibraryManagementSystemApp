using ELibrary.Domain.Models;
using ELibrary.Domain.Services;
using ELibrary.EF.DesginTimeFactory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.EF.Services
{
    public class GenericDataService<T> : IDataService<T> where T:DomainObject
    {
        private DesignTimeDbContextOptionsFactory factory;

        public GenericDataService(DesignTimeDbContextOptionsFactory factory)
        {
            this.factory = factory;
        }

        public async Task<T> Create(T entity)
        {
            using(var context = factory.CreateDbContext(null))
            {
              var newentity =  await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
                return newentity.Entity;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            using (var context = factory.CreateDbContext(null))
            {
               T entity =  await context.Set<T>().FirstOrDefaultAsync(i=>i.Id==id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<T> Get(Guid id)
        {
            using (var context = factory.CreateDbContext(null))
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync(i => i.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (var context = factory.CreateDbContext(null))
            {
                var newentity = await context.Set<T>().ToListAsync();
                return newentity;
            }
        }

        public async Task<T> Update(Guid id, T entity)
        {
            using (var context = factory.CreateDbContext(null))
            {
               entity.Id = id;

                 context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }
    }
}

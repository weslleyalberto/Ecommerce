using Domain.Interfaces.Generics;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Repository.Generics
{
    public class RepositoryGenerics<T> : IGeneric<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<ContextBase> _optionsBuilder;

        public RepositoryGenerics()
        {
            _optionsBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task Add(T objeto)
        {
            using(var data = new ContextBase(_optionsBuilder))
            {
                await data.Set<T>().AddAsync(objeto);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(T Objeto)
        {
            using (var data = new ContextBase(_optionsBuilder))
            {
                 data.Set<T>().Remove(Objeto);
                await data.SaveChangesAsync();
            }
        }

       

        public async Task<T> GetEntityById(int id)
        {
            using (var data = new ContextBase(_optionsBuilder))
            {
               return await data.Set<T>().FindAsync(id);
               
            }
        }

        public  async Task<List<T>> List()
        {
            using (var data = new ContextBase(_optionsBuilder))
            {
                return await data.Set<T>().AsNoTracking().ToListAsync();

            }
        }

        public async Task Update(T Objeto)
        {
            using (var data = new ContextBase(_optionsBuilder))
            {
                data.Set<T>().Update(Objeto);
                await data.SaveChangesAsync();
            }
        }
        public void Dispose()
        {
            _optionsBuilder?.Freeze();
        }
    }
}

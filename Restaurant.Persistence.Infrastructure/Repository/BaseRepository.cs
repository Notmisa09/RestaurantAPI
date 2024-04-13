using Microsoft.EntityFrameworkCore;
using Persistance.Restaurant.Infrastructure.Context;
using Restaurant.Core.Application.Interfaces.IRepositories;

namespace Persistence.Restaurant.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly RestContext _context;
        protected DbSet<T> _entities;
        public BaseRepository(RestContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int Id)
        {
            return await _context.Set<T>().FindAsync(Id);
        }

        public virtual async Task RemoveAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<List<T>> GetAllWithInclude(List<string> properties)
        {
            var query = _context.Set<T>().AsQueryable();

            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }

        public virtual async Task UpdateAsync(T entity, int Id)
        {
            var oldvalue = await _context.Set<T>().FindAsync(Id);
            _context.Entry(oldvalue).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
    }
}

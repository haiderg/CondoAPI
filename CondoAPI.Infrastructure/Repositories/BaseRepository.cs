using CondoAPI.Core.Interfaces;
using CondoAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CondoAPI.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly CondoDbContext _context;

        protected BaseRepository(CondoDbContext context)
        {
            _context = context;
        }
        
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<int> CreateAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return 1; // Return success indicator
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null) return false;
            
            _context.Set<T>().Remove(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
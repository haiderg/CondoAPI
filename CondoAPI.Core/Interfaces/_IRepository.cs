using CondoAPI.Core.Models;

namespace CondoAPI.Infrastructure.Interfaces
{
    public interface _IRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync();
        Task<int> CreateAsync(T entity);
    }
}

using Microsoft.EntityFrameworkCore;

namespace CondoAPI.Infrastructure.Data
{
    public interface IDbConnectionFactory
    {
        DbContextOptions<T> CreateDbContextOptions<T>() where T : DbContext;
    }
}
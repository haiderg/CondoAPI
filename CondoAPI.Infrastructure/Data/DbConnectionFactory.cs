using CondoAPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CondoAPI.Infrastructure.Data
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly DatabaseSettings _settings;

        public DbConnectionFactory(IOptions<DatabaseSettings> settings)
        {
            _settings = settings.Value;
        }

        public DbContextOptions<T> CreateDbContextOptions<T>() where T : DbContext
        {
            var builder = new DbContextOptionsBuilder<T>();
            
            // For now, only SQL Server is supported
            // To add other providers, install the respective NuGet packages:
            // - MySQL: Pomelo.EntityFrameworkCore.MySql
            // - PostgreSQL: Npgsql.EntityFrameworkCore.PostgreSQL
            // - SQLite: Microsoft.EntityFrameworkCore.Sqlite
            
            builder.UseSqlServer(_settings.ConnectionString);
            return builder.Options;
        }
    }
}
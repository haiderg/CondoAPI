using CondoAPI.Core.Interfaces;
using CondoAPI.Core.Models;
using CondoAPI.Infrastructure.Data;
using Dapper;


namespace CondoAPI.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly IDbConnectionFactory _connectionFactory;
        protected abstract string TableName { get; }

        protected BaseRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            var query = $"SELECT * FROM {TableName}";
            return await connection.QueryAsync<T>(query);
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var query = $"SELECT * FROM {TableName} WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<T>(query, new { Id = id });
        }

        public virtual async Task<int> CreateAsync(T entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            
            using var connection = _connectionFactory.CreateConnection();
            
            // This is a simplified implementation. In a real application, you would dynamically
            // generate the SQL based on the entity properties.
            var columns = GetColumns();
            var parameters = GetParameters();
            var values = GetParameterNames();
            
            var query = $"INSERT INTO {TableName} ({columns}) VALUES ({values}); SELECT CAST(SCOPE_IDENTITY() as int)";
            
            var id = await connection.QuerySingleAsync<int>(query, entity);
            entity.Id = id;
            
            return id;
        }

        public virtual async Task<bool> UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            
            using var connection = _connectionFactory.CreateConnection();
            
            // This is a simplified implementation. In a real application, you would dynamically
            // generate the SQL based on the entity properties.
            var updateQuery = GetUpdateQuery();
            
            var query = $"UPDATE {TableName} SET {updateQuery} WHERE Id = @Id";
            
            var rowsAffected = await connection.ExecuteAsync(query, entity);
            return rowsAffected > 0;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            var query = $"DELETE FROM {TableName} WHERE Id = @Id";
            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });
            return rowsAffected > 0;
        }

        // These methods should be overridden in derived classes to provide the correct column names and parameters
        protected abstract string GetColumns();
        protected abstract string GetParameters();
        protected abstract string GetParameterNames();
        protected abstract string GetUpdateQuery();
    }
}
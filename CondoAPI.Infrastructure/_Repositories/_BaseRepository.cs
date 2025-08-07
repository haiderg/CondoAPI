using CondoAPI.Core.Models;
using CondoAPI.Infrastructure._Data;
using CondoAPI.Infrastructure.Interfaces;
using Dapper;


namespace CondoAPI.Infrastructure._Repositories
{
    public abstract class _BaseRepository<T> : _IRepository<T> where T : BaseEntity
    {
        private readonly _IDbConnectionFactory _dbConnectionFactory;

        protected abstract string TableName { get; }

        public _BaseRepository(_IDbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateAsync(T entity)
        {
            var connection = _dbConnectionFactory.CreateConnection();

            var columns = GetColumns();
            var parameters = GetParameters();
            var values = GetParameterNames();

            entity.CreatedAt = DateTime.UtcNow;
            var query = $"INSERT INTO {TableName} ({columns}) VALUES ({values}); SELECT CAST(SCOPE_IDENTITY() as int)";

            var id = await connection.QuerySingleAsync<int>(query, entity);
            entity.Id = id;
            return id;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync()
        {
            throw new NotImplementedException();
        }

        // These methods should be overridden in derived classes to provide the correct column names and parameters
        protected abstract string GetColumns();
        protected abstract string GetParameters();
        protected abstract string GetParameterNames();
        protected abstract string GetUpdateQuery();
            
    }
}

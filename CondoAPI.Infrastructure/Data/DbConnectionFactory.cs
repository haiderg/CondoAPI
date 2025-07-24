using CondoAPI.Core.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace CondoAPI.Infrastructure.Data
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }

    public class SqlConnectionFactory : IDbConnectionFactory
    {
        private readonly DatabaseSettings _settings;

        public SqlConnectionFactory(IOptions<DatabaseSettings> settings)
        {
            _settings = settings.Value;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_settings.ConnectionString);
        }
    }
}


using CondoAPI.Core.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.Common;

namespace CondoAPI.Infrastructure._Data
{
    public interface _IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }

    public class _SQLConnectionFactory : _IDbConnectionFactory
    {
        private readonly _DatabaseSettings _settings;

        public _SQLConnectionFactory(IOptions<_DatabaseSettings> settings)
        {
            _settings = settings.Value;
        }    

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_settings.ConnectionString);
        }
    }
}

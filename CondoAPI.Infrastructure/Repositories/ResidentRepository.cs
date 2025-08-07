using CondoAPI.Core.Interfaces;
using CondoAPI.Core.Models;
using CondoAPI.Infrastructure.Data;
using Dapper;

namespace CondoAPI.Infrastructure.Repositories
{
    public class ResidentRepository : BaseRepository<Resident>, IResidentRepository
    {
        protected override string TableName => "Residents";

        public ResidentRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        public async Task<IEnumerable<Resident>> GetActiveResidentsAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            var query = $"SELECT * FROM {TableName} WHERE IsActive = 1";
            return await connection.QueryAsync<Resident>(query);
        }

        protected override string GetColumns()
        {
            return "Name, Email, ApartmentNumber, Phone, IsActive, CreatedAt, UpdatedAt";
        }

        protected override string GetParameters()
        {
            return "@Name, @Email, @ApartmentNumber, @Phone, @IsActive, @CreatedAt, @UpdatedAt";
        }

        protected override string GetParameterNames()
        {
            return "@Name, @Email, @ApartmentNumber, @Phone, @IsActive, @CreatedAt, @UpdatedAt";
        }

        protected override string GetUpdateQuery()
        {
            return "Name = @Name, Email = @Email, ApartmentNumber = @ApartmentNumber, Phone = @Phone, IsActive = @IsActive, UpdatedAt = @UpdatedAt";
        }
    }
}
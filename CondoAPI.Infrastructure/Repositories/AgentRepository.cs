using CondoAPI.Core.Interfaces;
using CondoAPI.Core.Models;

using CondoAPI.Infrastructure.Data;
using Dapper;


namespace CondoAPI.Infrastructure.Repositories
{
    public class AgentRepository : BaseRepository<Agent>, IAgentRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public AgentRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        protected override string TableName => "Agent";

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Agent>> GetAllAgentsAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            var query = $"SELECT * FROM {TableName}";
            return await connection.QueryAsync<Agent>(query);
        }

        public Task<Agent?> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Agent entity)
        {
            throw new NotImplementedException();
        }

        protected override string GetColumns()
        {
            return "AgentName,AgentAddress,AgentCity,AgentStateProvice,AgentPostalCode,AgentCountry,AgentPhone,AgentEmail,AgentContact";
        }

        protected override string GetParameterNames()
        {
            return "@AgentName,@AgentAddress,@AgentCity,@AgentStateProvice,@AgentPostalCode,@AgentCountry,@AgentPhone,@AgentEmail,@AgentContact";
        }

        protected override string GetParameters()
        {
            return "@AgentName,@AgentAddress,@AgentCity,@AgentStateProvice,@AgentPostalCode,@AgentCountry,@AgentPhone,@AgentEmail,@AgentContact";
        }

        protected override string GetUpdateQuery()
        {
            return "AgentName=@AgentName,AgentAddress=@AgentAddress,AgentCity=@AgentCity,AgentStateProvice=@AgentStateProvice,AgentPostalCode=@AgentPostalCode,AgentCountry=@AgentCountry,AgentPhone=@AgentPhone,AgentEmail=@AgentEmail,AgentContact=@AgentContact";
        }
    }
}

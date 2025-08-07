using CondoAPI.Core.Models;
using CondoAPI.Infrastructure._Data;
using CondoAPI.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoAPI.Infrastructure._Repositories
{
    public class AgentRepository : _BaseRepository<Agent>
    {
        public AgentRepository(_IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {

        }

        protected override string TableName => "Agent";

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

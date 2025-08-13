using CondoAPI.Core.Interfaces;
using CondoAPI.Core.Models;
using CondoAPI.Infrastructure.Data;

namespace CondoAPI.Infrastructure.Repositories
{
    public class AgentRepository : BaseRepository<Agent>, IAgentRepository
    {
        public AgentRepository(CondoDbContext context) : base(context)
        {
        }
    }
}
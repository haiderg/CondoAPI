using CondoAPI.Core.Interfaces;
using CondoAPI.Core.Models;

namespace CondoAPI.Infrastructure.Services
{
    public class AgentService : IAgentService
    {
        private readonly IAgentRepository _agentRepository;

        public AgentService(IAgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        public async Task<IEnumerable<Agent>> GetAgentsAsync()
        {
            return await _agentRepository.GetAllAsync();
        }
    }
}
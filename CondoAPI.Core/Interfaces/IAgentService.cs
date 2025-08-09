using CondoAPI.Core.Models;

namespace CondoAPI.Core.Interfaces
{
    public interface IAgentService
    {
        Task<IEnumerable<Agent>> GetAgentsAsync();
    }
}
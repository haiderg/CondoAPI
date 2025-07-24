using CondoAPI.Core.Models;

namespace CondoAPI.Core.Interfaces
{
    public interface IResidentRepository : IRepository<Resident>
    {
        Task<IEnumerable<Resident>> GetActiveResidentsAsync();
    }
}
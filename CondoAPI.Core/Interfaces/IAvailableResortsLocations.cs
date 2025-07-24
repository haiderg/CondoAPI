using CondoAPI.Core.Models;
using CondoAPI.Core.DTOs.Requests;
using CondoAPI.Core.DTOs.Responses;

namespace CondoAPI.Core.Interfaces
{
    public interface IAvailableResortsLocations : IRepository<AvailableResortsLocations>
    {
        Task<IEnumerable<AvailableResortsCountriesResponse>> GetAvailableResortsCountriesByFiltersAsync(AvailableResortsCountriesRequest request);
    }
}
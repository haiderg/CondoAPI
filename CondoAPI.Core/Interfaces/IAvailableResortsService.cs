using CondoAPI.Core.DTOs.Requests;
using CondoAPI.Core.DTOs.Responses;

namespace CondoAPI.Core.Interfaces
{
    public interface IAvailableResortsService
    {
        Task<IEnumerable<AvailableResortsCountriesResponse>> GetAvailableResortsCountriesAsync(AvailableResortsCountriesRequest request);
    }
}
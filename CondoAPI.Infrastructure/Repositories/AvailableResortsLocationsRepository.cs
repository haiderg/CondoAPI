using CondoAPI.Core.Interfaces;
using CondoAPI.Core.Models;
using CondoAPI.Core.DTOs.Requests;
using CondoAPI.Core.DTOs.Responses;
using CondoAPI.Infrastructure.Data;
using Dapper;
using System.Data;

namespace CondoAPI.Infrastructure.Repositories
{
    public class AvailableResortsLocationsRepository : BaseRepository<AvailableResortsLocations>, IAvailableResortsLocations
    {
        public AvailableResortsLocationsRepository(DbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<IEnumerable<AvailableResortsCountriesResponse>> GetAvailableResortsCountriesByFiltersAsync(AvailableResortsCountriesRequest request)
        {
            using var connection = _connectionFactory.CreateConnection();
            
            var parameters = new DynamicParameters();
            parameters.Add("@ArrivalDate", request.ArrivalDate);
            parameters.Add("@DepartureDate", request.DepartureDate);
            parameters.Add("@MaxOccupancy", request.MaxOccupancy);

            return await connection.QueryAsync<AvailableResortsCountriesResponse>(
                "YourStoredProcedureName", // Replace with your SP name
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }
    }
}
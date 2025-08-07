using CondoAPI.Core.Interfaces;
using CondoAPI.Core.DTOs.Requests;
using CondoAPI.Core.DTOs.Responses;
using CondoAPI.Infrastructure.Data;
using Dapper;
using System.Data;

namespace CondoAPI.Infrastructure.Services
{
    public class AvailableResortsService : IAvailableResortsService
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public AvailableResortsService(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<AvailableResortsCountriesResponse>> GetAvailableResortsCountriesAsync(AvailableResortsCountriesRequest request)
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
using CondoAPI.Core.Interfaces;
using CondoAPI.Core.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CondoAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvailableResortsController : ControllerBase
    {
        private readonly IAvailableResortsLocations _repository;

        public AvailableResortsController(IAvailableResortsLocations repository)
        {
            _repository = repository;
        }

        [HttpPost("countries")]
        public async Task<IActionResult> GetAvailableCountries([FromBody] AvailableResortsCountriesRequest request)
        {
            var countries = await _repository.GetAvailableResortsCountriesByFiltersAsync(request);
            return Ok(countries);
        }
    }
}
using CondoAPI.Core.Interfaces;
using CondoAPI.Core.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CondoAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvailableResortsController : ControllerBase
    {
        private readonly IAvailableResortsService _service;

        public AvailableResortsController(IAvailableResortsService service)
        {
            _service = service;
        }

        [HttpPost("countries")]
        public async Task<IActionResult> GetAvailableCountries([FromBody] AvailableResortsCountriesRequest request)
        {
            var countries = await _service.GetAvailableResortsCountriesAsync(request);
            return Ok(countries);
        }
    }
}
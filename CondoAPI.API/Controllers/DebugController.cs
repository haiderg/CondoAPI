using CondoAPI.Core.Models;
using CondoAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CondoAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebugController : ControllerBase
    {
        private readonly DatabaseSettings _settings;
        private readonly CondoDbContext _context;

        public DebugController(IOptions<DatabaseSettings> settings, CondoDbContext context)
        {
            _settings = settings.Value;
            _context = context;
        }

        [HttpGet("connection-info")]
        public IActionResult GetConnectionInfo()
        {
            return Ok(new
            {
                ConnectionString = _settings.ConnectionString,
                DatabaseProvider = _settings.DatabaseProvider
            });
        }
    }
}
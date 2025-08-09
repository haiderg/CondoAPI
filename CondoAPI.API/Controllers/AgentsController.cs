using CondoAPI.Core.Interfaces;
using CondoAPI.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CondoAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AgentsController : ControllerBase
    {
        private readonly IAgentService _agentService;
        private readonly ILogger<AgentsController> _logger;

        public AgentsController(IAgentService agentService, ILogger<AgentsController> logger)
        {
            _agentService = agentService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Agent>>> GetAllAgents()
        {
            _logger.LogInformation("Getting all agents");
            var agents = await _agentService.GetAgentsAsync();
            return Ok(agents);
        }
    }
}
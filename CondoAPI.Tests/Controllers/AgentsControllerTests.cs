using CondoAPI.API.Controllers;
using CondoAPI.Core.Interfaces;
using CondoAPI.Core.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CondoAPI.Tests.Controllers
{
    public class AgentsControllerTests
    {
        private readonly Mock<IAgentService> _mockAgentService;
        private readonly Mock<ILogger<AgentsController>> _mockLogger;
        private readonly AgentsController _controller;

        public AgentsControllerTests()
        {
            _mockAgentService = new Mock<IAgentService>();
            _mockLogger = new Mock<ILogger<AgentsController>>();
            _controller = new AgentsController(_mockAgentService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllAgents_ReturnsOkResult_WithListOfAgents()
        {
            // Arrange
            var agents = new List<Agent>
            {
                new Agent { AgentID = 1, AgentName = "John Agent", AgentEmail = "john@agent.com" },
                new Agent { AgentID = 2, AgentName = "Jane Agent", AgentEmail = "jane@agent.com" }
            };
            _mockAgentService.Setup(service => service.GetAgentsAsync()).ReturnsAsync(agents);

            // Act
            var result = await _controller.GetAllAgents();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedAgents = okResult.Value.Should().BeAssignableTo<IEnumerable<Agent>>().Subject;

            returnedAgents.Should().HaveCount(2);
            returnedAgents.Should().Contain(a => a.AgentID == 1 && a.AgentName == "John Agent");
            returnedAgents.Should().Contain(a => a.AgentID == 2 && a.AgentName == "Jane Agent");
        }

        [Fact]
        public async Task GetAllAgents_ReturnsOkResult_WithEmptyList_WhenNoAgents()
        {
            // Arrange
            var emptyAgents = new List<Agent>();
            _mockAgentService.Setup(service => service.GetAgentsAsync()).ReturnsAsync(emptyAgents);

            // Act
            var result = await _controller.GetAllAgents();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedAgents = okResult.Value.Should().BeAssignableTo<IEnumerable<Agent>>().Subject;
            returnedAgents.Should().BeEmpty();
        }
    }
}
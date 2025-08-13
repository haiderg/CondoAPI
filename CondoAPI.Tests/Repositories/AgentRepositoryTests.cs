using CondoAPI.Core.Models;
using CondoAPI.Infrastructure.Data;
using CondoAPI.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CondoAPI.Tests.Repositories
{
    public class AgentRepositoryTests : IDisposable
    {
        private readonly CondoDbContext _context;
        private readonly AgentRepository _repository;

        public AgentRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CondoDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new CondoDbContext(options);
            _repository = new AgentRepository(_context);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAgents_WhenDataExists()
        {
            // Arrange
            var agents = new List<Agent>
            {
                new Agent { AgentID = 1, AgentName = "John Agent", AgentEmail = "john@agent.com" },
                new Agent { AgentID = 2, AgentName = "Jane Agent", AgentEmail = "jane@agent.com" }
            };

            _context.Agents.AddRange(agents);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsEmptyList_WhenNoDataExists()
        {
            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
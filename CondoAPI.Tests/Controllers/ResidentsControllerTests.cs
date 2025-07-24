using CondoAPI.API.Controllers;
using CondoAPI.Core.Exceptions;
using CondoAPI.Core.Interfaces;
using CondoAPI.Core.Models;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CondoAPI.Tests.Controllers
{
    public class ResidentsControllerTests
    {
        private readonly Mock<IResidentRepository> _mockRepository;
        private readonly Mock<IValidator<Resident>> _mockValidator;
        private readonly Mock<ILogger<ResidentsController>> _mockLogger;
        private readonly ResidentsController _controller;

        public ResidentsControllerTests()
        {
            _mockRepository = new Mock<IResidentRepository>();
            _mockValidator = new Mock<IValidator<Resident>>();
            _mockLogger = new Mock<ILogger<ResidentsController>>();
            _controller = new ResidentsController(_mockRepository.Object, _mockValidator.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetAllResidents_ReturnsOkResult_WithListOfResidents()
        {
            // Arrange
            var residents = new List<Resident>
            {
                new Resident { Id = 1, Name = "John Doe", Email = "john@example.com", ApartmentNumber = "101" },
                new Resident { Id = 2, Name = "Jane Smith", Email = "jane@example.com", ApartmentNumber = "102" }
            };
            _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(residents);

            // Act
            var result = await _controller.GetAllResidents();

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedResidents = okResult.Value.Should().BeAssignableTo<IEnumerable<Resident>>().Subject;
            returnedResidents.Should().HaveCount(2);
            returnedResidents.Should().Contain(r => r.Id == 1 && r.Name == "John Doe");
            returnedResidents.Should().Contain(r => r.Id == 2 && r.Name == "Jane Smith");
        }

        [Fact]
        public async Task GetResidentById_WithValidId_ReturnsOkResult_WithResident()
        {
            // Arrange
            var resident = new Resident { Id = 1, Name = "John Doe", Email = "john@example.com", ApartmentNumber = "101" };
            _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(resident);

            // Act
            var result = await _controller.GetResidentById(1);

            // Assert
            var okResult = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            var returnedResident = okResult.Value.Should().BeOfType<Resident>().Subject;
            returnedResident.Id.Should().Be(1);
            returnedResident.Name.Should().Be("John Doe");
        }

        [Fact]
        public async Task GetResidentById_WithInvalidId_ThrowsNotFoundException()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetByIdAsync(999)).ReturnsAsync((Resident)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _controller.GetResidentById(999));
        }

        [Fact]
        public async Task CreateResident_WithValidResident_ReturnsCreatedAtAction()
        {
            // Arrange
            var resident = new Resident { Name = "John Doe", Email = "john@example.com", ApartmentNumber = "101" };
            _mockValidator.Setup(v => v.ValidateAsync(resident, default)).ReturnsAsync(new ValidationResult());
            _mockRepository.Setup(repo => repo.CreateAsync(resident)).ReturnsAsync(1);

            // Act
            var result = await _controller.CreateResident(resident);

            // Assert
            var createdAtActionResult = result.Result.Should().BeOfType<CreatedAtActionResult>().Subject;
            createdAtActionResult.ActionName.Should().Be(nameof(ResidentsController.GetResidentById));
            createdAtActionResult.RouteValues["id"].Should().Be(1);
            var returnedResident = createdAtActionResult.Value.Should().BeOfType<Resident>().Subject;
            returnedResident.Should().Be(resident);
        }

        [Fact]
        public async Task CreateResident_WithInvalidResident_ThrowsBadRequestException()
        {
            // Arrange
            var resident = new Resident { Name = "", Email = "invalid-email", ApartmentNumber = "" };
            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("Name", "Name is required"),
                new ValidationFailure("Email", "A valid email address is required")
            };
            _mockValidator.Setup(v => v.ValidateAsync(resident, default))
                .ReturnsAsync(new ValidationResult(validationFailures));

            // Act & Assert
            await Assert.ThrowsAsync<BadRequestException>(() => _controller.CreateResident(resident));
        }
    }
}
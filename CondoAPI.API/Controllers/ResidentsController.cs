using CondoAPI.Core.Exceptions;
using CondoAPI.Core.Interfaces;
using CondoAPI.Core.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace CondoAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResidentsController : ControllerBase
    {
        private readonly IResidentRepository _residentRepository;
        private readonly IValidator<Resident> _validator;
        private readonly ILogger<ResidentsController> _logger;

        public ResidentsController(
            IResidentRepository residentRepository,
            IValidator<Resident> validator,
            ILogger<ResidentsController> logger)
        {
            _residentRepository = residentRepository;
            _validator = validator;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resident>>> GetAllResidents()
        {
            _logger.LogInformation("Getting all residents");
            var residents = await _residentRepository.GetAllAsync();
            return Ok(residents);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Resident>>> GetActiveResidents()
        {
            _logger.LogInformation("Getting active residents");
            var residents = await _residentRepository.GetActiveResidentsAsync();
            return Ok(residents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Resident>> GetResidentById(int id)
        {
            _logger.LogInformation("Getting resident with ID: {ResidentId}", id);
            var resident = await _residentRepository.GetByIdAsync(id);
            
            if (resident == null)
            {
                _logger.LogWarning("Resident with ID: {ResidentId} not found", id);
                throw new NotFoundException($"Resident with ID {id} not found");
            }
            
            return Ok(resident);
        }

        [HttpPost]
        public async Task<ActionResult<Resident>> CreateResident(Resident resident)
        {
            _logger.LogInformation("Creating a new resident");
            
            var validationResult = await _validator.ValidateAsync(resident);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for resident creation: {Errors}", 
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                throw new BadRequestException(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }
            
            var id = await _residentRepository.CreateAsync(resident);
            _logger.LogInformation("Resident created with ID: {ResidentId}", id);
            
            return CreatedAtAction(nameof(GetResidentById), new { id }, resident);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResident(int id, Resident resident)
        {
            _logger.LogInformation("Updating resident with ID: {ResidentId}", id);
            
            if (id != resident.Id)
            {
                _logger.LogWarning("ID mismatch in update request");
                throw new BadRequestException("ID mismatch");
            }
            
            var validationResult = await _validator.ValidateAsync(resident);
            if (!validationResult.IsValid)
            {
                _logger.LogWarning("Validation failed for resident update: {Errors}", 
                    string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
                throw new BadRequestException(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }
            
            var existingResident = await _residentRepository.GetByIdAsync(id);
            if (existingResident == null)
            {
                _logger.LogWarning("Resident with ID: {ResidentId} not found for update", id);
                throw new NotFoundException($"Resident with ID {id} not found");
            }
            
            var result = await _residentRepository.UpdateAsync(resident);
            if (!result)
            {
                _logger.LogError("Failed to update resident with ID: {ResidentId}", id);
                throw new ApiException("Failed to update resident");
            }
            
            _logger.LogInformation("Resident with ID: {ResidentId} updated successfully", id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResident(int id)
        {
            _logger.LogInformation("Deleting resident with ID: {ResidentId}", id);
            
            var existingResident = await _residentRepository.GetByIdAsync(id);
            if (existingResident == null)
            {
                _logger.LogWarning("Resident with ID: {ResidentId} not found for deletion", id);
                throw new NotFoundException($"Resident with ID {id} not found");
            }
            
            var result = await _residentRepository.DeleteAsync(id);
            if (!result)
            {
                _logger.LogError("Failed to delete resident with ID: {ResidentId}", id);
                throw new ApiException("Failed to delete resident");
            }
            
            _logger.LogInformation("Resident with ID: {ResidentId} deleted successfully", id);
            return NoContent();
        }
    }
}
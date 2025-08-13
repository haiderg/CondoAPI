using CondoAPI.Core.Models;
using FluentValidation;

namespace CondoAPI.Core.Validators.Models
{
    public class AgentValidator : AbstractValidator<Agent>
    {
        public AgentValidator()
        {
            RuleFor(x => x.AgentName)
                .NotEmpty()
                .WithMessage("Agent name is required")
                .MaximumLength(100)
                .WithMessage("Agent name cannot exceed 100 characters");

            RuleFor(x => x.AgentEmail)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.AgentEmail))
                .WithMessage("Invalid email format");

            RuleFor(x => x.AgentPhone)
                .Matches(@"^\+?[\d\s\-\(\)]+$")
                .When(x => !string.IsNullOrEmpty(x.AgentPhone))
                .WithMessage("Invalid phone number format");
        }
    }
}
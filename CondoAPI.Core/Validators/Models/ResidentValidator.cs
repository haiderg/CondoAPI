using CondoAPI.Core.Models;
using FluentValidation;

namespace CondoAPI.Core.Validators.Models
{
    public class ResidentValidator : AbstractValidator<Resident>
    {
        public ResidentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("A valid email address is required")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

            RuleFor(x => x.ApartmentNumber)
                .NotEmpty().WithMessage("Apartment number is required")
                .MaximumLength(20).WithMessage("Apartment number cannot exceed 20 characters");

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Phone number is required")
                .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters");
        }
    }
}
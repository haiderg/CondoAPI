using CondoAPI.Core.DTOs.Requests;
using FluentValidation;

namespace CondoAPI.Core.Validators.DTOs
{
    public class AvailableResortsCountriesRequestValidator : AbstractValidator<AvailableResortsCountriesRequest>
    {
        public AvailableResortsCountriesRequestValidator()
        {
            RuleFor(x => x.ArrivalDate)
                .NotEmpty().WithMessage("Arrival Date is required.");

            RuleFor(x => x.DepartureDate)
                .NotEmpty().WithMessage("Departure Date is required.")
                .GreaterThan(x => x.ArrivalDate).WithMessage("Departure Date must be after Arrival Date.");

            RuleFor(x => x.MaxOccupancy)
                .GreaterThan(0).WithMessage("Max Occupancy must be greater than 0.");
        }
    }
}

using CondoAPI.Core.DTOs;
using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoAPI.Core.Validators
{
    public class AvailableResortsLocationsValidator : AbstractValidator<AvailableResortsLocations>
    {
        public AvailableResortsLocationsValidator()
        {
            RuleFor(x => x.ArrivalDate).NotEmpty().WithMessage("Arrival Date is required.");
            RuleFor(x => x.DepartureDate).NotEmpty().WithMessage("Departure Date is required.");
            RuleFor(x => x.MaxOccupancy).NotEmpty().WithMessage("Max Occupancy is required.");
        }

    }
}

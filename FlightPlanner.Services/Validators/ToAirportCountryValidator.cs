using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators
{
    public class ToAirportCountryValidator : IValidator
    {
        public bool IsValid(AddFlightDto dto)
        {
            return !string.IsNullOrEmpty(dto?.To?.Country);
        }
    }
}

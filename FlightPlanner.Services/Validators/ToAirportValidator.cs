using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.Validators
{
    public class ToAirportValidator : IValidator
    {
        public bool IsValid(AddFlightDto dto)
        {
            return dto?.To != null;
        }
    }
}

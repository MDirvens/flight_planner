using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.SearchValidators
{
    public class SearchDepartureTimeValidator : ISearchValidator
    {
        public bool IsValidSearch(SearchFlightsDto dto)
        {
            return dto.DepartureDate != null;
        }
    }
}

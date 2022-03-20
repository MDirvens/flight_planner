using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.SearchValidators
{
    public class SearchToValidator : ISearchValidator
    {
        public bool IsValidSearch(SearchFlightsDto dto)
        {
            return dto.To != null;
        }
    }
}

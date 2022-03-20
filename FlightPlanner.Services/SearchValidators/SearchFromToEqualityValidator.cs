using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Services.SearchValidators
{
    public class SearchFromToEqualityValidator : ISearchValidator
    {
        public bool IsValidSearch(SearchFlightsDto dto)
        {
            return dto.From != dto.To;
        }
    }
}

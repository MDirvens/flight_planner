using FlightPlanner.Core.Dto;

namespace FlightPlanner.Core.Services
{
    public interface ISearchValidator
    {
        bool IsValidSearch(SearchFlightsDto dto);
    }
}

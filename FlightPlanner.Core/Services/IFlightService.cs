using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight GetFlightWithAirports(int id);
        void DeleteFlightById(int id);
        bool Exist(AddFlightDto dto);
    }
}

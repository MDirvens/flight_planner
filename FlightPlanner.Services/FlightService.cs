using System.Linq;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(IFlightPlanerDbContext context) : base(context)
        {
        }

        public Flight GetFlightWithAirports(int id)
        {
            return Query()
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);
        }

        public void DeleteFlightById(int id)
        {
            var flight = GetFlightWithAirports(id);

            if (flight != null)
                Delete(flight);
        }

        public bool Exist(AddFlightDto dto)
        {
            return Query().Any(f =>
                f.Carrier.ToLower().Trim() == dto.Carrier.ToLower().Trim() &&
                f.DepartureTime == dto.DepartureTime &&
                f.ArrivalTime == dto.ArrivalTime &&
                f.To.AirportName.ToLower().Trim() == dto.To.Airport.ToLower().Trim() &&
                f.From.AirportName.ToLower().Trim() == dto.From.Airport.ToLower().Trim());
        }
    }
}

using System.Collections.Generic;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IAirportService : IEntityService<Airport>
    {
        List<AddAirportDto> GetAirports(string search);
        List<AddAirportDto> ConvertAirportList(List<Airport> airports);
    }
}

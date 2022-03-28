using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;


namespace FlightPlanner.Services
{
    public class AirportService : EntityService<Airport>, IAirportService
    {
        private IMapper _mapper;

        public AirportService(IMapper mapper,IFlightPlanerDbContext context) : base(context)
        {
            _mapper = mapper;
        }

        public List<AddAirportDto> GetAirports(string search)
        {
            var airport = search.ToLower().Trim();
            List<Airport> airports =  Query().Where(f =>
                    f.AirportName.ToLower().Trim().Contains(airport) ||
                    f.City.ToLower().Trim().Contains(airport) ||
                    f.Country.ToLower().Trim().Contains(airport)).
                Select(a => a).ToList();

            return ConvertAirportList(airports);
        }

        public List<AddAirportDto> ConvertAirportList(List<Airport> airports)
        {
            List<AddAirportDto> convertedAirports = new();
            airports.ForEach(a => convertedAirports.Add(_mapper.Map<AddAirportDto>(a)));
            return convertedAirports;
        }
    }
}

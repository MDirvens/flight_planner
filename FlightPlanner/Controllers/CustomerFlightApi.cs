using System.Collections.Generic;
using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [EnableCors]
    [ApiController]
    public class CustomerFlightApi : ControllerBase
    {
        private readonly FlightPlanerDbContext _context;

        public CustomerFlightApi(FlightPlanerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Airports")]
        public IActionResult SearchAirports(string search)
        {
            var airport = search.ToLower().Trim();

            var airports = _context.Airports.Where(f =>
                    f.AirportName.ToLower().Trim().Contains(airport) ||
                    f.City.ToLower().Trim().Contains(airport) ||
                    f.Country.ToLower().Trim().Contains(airport)).
                Select(a => a).ToList();

            return Ok(airports);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            var flight = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);

            if (flight is null)
                return NotFound();

            return Ok(flight);
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(SearchFlightsRequest req)
        {
           if(!FlightStorage.RequestValidFlights(req))
               return BadRequest();

           var pageResult = new PageResult
           {
                Items = new List<Flight>(_context.Flights
                    .Include(f => f.From)
                    .Include(f => f.To)),
                TotalItems = _context.Flights.Count(),
                Page = _context.Flights.Count()
           };

            return Ok(pageResult);
        }
    }
}

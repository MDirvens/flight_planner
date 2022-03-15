using System.Linq;
using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [EnableCors]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        private readonly FlightPlanerDbContext _context;
        private static readonly object _lock = new();

        public AdminApiController(FlightPlanerDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]
        [Route("flights/{id}")]
        public IActionResult GetFlights(int id)
        {
            var flight = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);

            if (flight is null)
                return NotFound();

            return Ok(flight);
        }

        [HttpPut]
        [Authorize]
        [Route("flights")]
        public IActionResult PutFlights(AddFlightRequest request)
        {
            lock (_lock)
            {

                if (!FlightStorage.IsValid(request))
                {
                    return BadRequest();
                }


                if (Exist(request))
                {
                    return Conflict();
                }


                var flight = FlightStorage.ConvertToFlight(request);
                _context.Flights.Add(flight);
                _context.SaveChanges();

                return Created("", flight);
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            var flight = _context.Flights
                .Include(f => f.From)
                .Include(f => f.To)
                .SingleOrDefault(f => f.Id == id);

            if (flight != null)
            {
                _context.Flights.Remove(flight);
                _context.SaveChanges();
            }

            return Ok();
        }

        private bool Exist(AddFlightRequest request)
        {
            return _context.Flights.Any(f =>
                f.Carrier.ToLower().Trim() == request.Carrier.ToLower().Trim() &&
                f.DepartureTime == request.DepartureTime &&
                f.ArrivalTime == request.ArrivalTime &&
                f.To.AirportName.ToLower().Trim() == request.To.AirportName.ToLower().Trim() &&
                f.From.AirportName.ToLower().Trim() == request.From.AirportName.ToLower().Trim());
        }
    }
}

using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomerFlightApi : ControllerBase
    {
        [HttpGet]
        [Route("Airports")]
        public IActionResult SearchAirports(string search)
        {
            var airports = FlightStorage.FindAirports(search);

            return Ok(airports);
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            var flight = FlightStorage.GetFlight(id);

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
           
           var pageResults = FlightStorage.FindFlights(req);

           return Ok(pageResults);
        }
    }
}

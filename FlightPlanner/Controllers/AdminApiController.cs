using FlightPlanner.Models;
using FlightPlanner.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [ApiController]
    public class AdminApiController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        [Route("flights/{id}")]
        public IActionResult GetFlights(int id)
        {
            var flight = FlightStorage.GetFlight(id);

            if (flight is null)
                return NotFound();

            return Ok();
        }

        [HttpPut]
        [Authorize]
        [Route("flights")]
        public IActionResult PutFlights(AddFlightRequest request)
        {
                if (!FlightStorage.IsValid(request))
                {
                    return BadRequest();
                }

                if (FlightStorage.Exist(request))
                {
                    return Conflict();
                }

                var flight = FlightStorage.AddFlight(request);

                return Created("", flight);
        }

        [HttpDelete]
        [Authorize]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            FlightStorage.DeleteFlight(id);

            return Ok();
        }
    }
}

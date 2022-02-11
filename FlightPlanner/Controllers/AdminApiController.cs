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
        private readonly object _object = FlightStorage._flights; 

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
            lock (_object)
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
        }

        [HttpDelete]
        [Authorize]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            lock (_object)
            {
                FlightStorage.DeleteFlight(id);

                return Ok();
            }
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Controllers
{
    [Route("admin-api")]
    [EnableCors]
    [ApiController]
    public class ApiController : ApiControllerBase
    {
        public ApiController(IFlightService flightService,
                             IEnumerable<IValidator> validators,
                             IMapper mapper)
        {
            _flightService = flightService;
            _validators = validators;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        [Route("flights/{id}")]
        public IActionResult GetFlights(int id)
        {
            var flight = _flightService.GetFlightWithAirports(id);

            return flight == null ? NotFound() : Ok(flight);
        }

        [HttpPut]
        [Authorize]
        [Route("flights")]
        public IActionResult PutFlights(AddFlightDto dto)
        {
            lock (_lock)
            {

                if (!_validators.All(v => v.IsValid(dto)))
                {
                    return BadRequest();
                }

                if (_flightService.Exist(dto))
                {
                    return Conflict();
                }

                var flight = _mapper.Map<Flight>(dto);
                _flightService.Create(flight);

                return Created("", _mapper.Map<AddFlightDto>(flight));
            }
        }

        [HttpDelete]
        [Authorize]
        [Route("flights/{id}")]
        public IActionResult DeleteFlight(int id)
        {
            _flightService.DeleteFlightById(id);

            return Ok();
        }
    }
}

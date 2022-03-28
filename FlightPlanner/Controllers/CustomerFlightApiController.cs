using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;

namespace FlightPlanner.Controllers
{
    [Route("api")]
    [EnableCors]
    [ApiController]
    public class CustomerFlightApiController : ApiControllerBase
    {
        public CustomerFlightApiController(IFlightService flightService, 
                                           IAirportService airportService,
                                           IPageResult pageResult,
                                           IEnumerable<ISearchValidator> searchValidators,
                                           IMapper mapper)
        {
            _flightService = flightService;
            _airportService = airportService;
            _searchValidators = searchValidators;
            _mapper = mapper;
            _pageResult = pageResult;
        }

        [HttpGet]
        [Route("Airports")]
        public IActionResult SearchAirports(string search)
        {
            return Ok(_airportService.GetAirports(search));
        }

        [HttpGet]
        [Route("flights/{id}")]
        public IActionResult FindFlightById(int id)
        {
            var flight = _flightService.GetFlightWithAirports(id);

            if (flight is null)
                return NotFound();

            return Ok(_mapper.Map<AddFlightDto>(flight));
        }

        [HttpPost]
        [Route("flights/search")]
        public IActionResult SearchFlights(SearchFlightsDto dto)
        {
            if (!_searchValidators.All(v => v.IsValidSearch(dto)))
                return BadRequest();

            return Ok(_pageResult.GetPageResult());
        }
    }
}

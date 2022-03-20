using System.Collections.Generic;
using AutoMapper;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlanner.Core.Models
{
    public abstract class ApiControllerBase : ControllerBase
    {
        public static readonly object _lock = new();

        protected IFlightService _flightService;
        protected IEnumerable<IValidator> _validators;
        protected IMapper _mapper;
        protected IAirportService _airportService;
        protected IPageResult _pageResult;
        protected IEnumerable<ISearchValidator> _searchValidators;
        protected IDbExtendedService _service;
    }
}

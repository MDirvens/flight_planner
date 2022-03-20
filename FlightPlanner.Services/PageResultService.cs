using System.Collections.Generic;
using System.Linq;
using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class PageResultService : EntityService<Flight>, IPageResult
    {
        public PageResultService(IFlightPlanerDbContext context) : base(context)
        {
        }

        public PageResultDto GetPageResult()
        {
            var pageResult = new PageResultDto
            {
                Items = new List<Flight>(Query()
                    .Include(f => f.From)
                    .Include(f => f.To)),
                TotalItems = Query().Count(),
                Page = Query().Count()

            };

            return pageResult;
        }
    }
}

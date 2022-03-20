using System.Collections.Generic;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Dto
{
    public class PageResultDto
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public List<Flight> Items { get; set; }
    }
}

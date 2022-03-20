using FlightPlanner.Core.Dto;
using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IPageResult : IEntityService<Flight>
    {
        PageResultDto GetPageResult();
    }
}

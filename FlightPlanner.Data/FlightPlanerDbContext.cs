using System.Threading.Tasks;
using FlightPlanner.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Data
{
    public class FlightPlanerDbContext : DbContext, IFlightPlanerDbContext
    {
        public FlightPlanerDbContext(DbContextOptions options) : base(options){}
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}

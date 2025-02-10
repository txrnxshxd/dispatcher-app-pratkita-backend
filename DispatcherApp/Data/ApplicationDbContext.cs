using DispatcherApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace DispatcherApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
        
        }

        public DbSet<Models.Plane> Planes { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<PlaneType> PlaneTypes { get; set; }
        public DbSet<CarrierCompany> CarrierCompanies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("dispatcher");
        }
    }
}

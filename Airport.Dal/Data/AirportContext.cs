using AirPort.Common;
using AirPort.Common.Models;
using AirPort.Common.Models.Api.StationApi;
using AirPort.Common.Models.Stations;
using Common.Models;
using Common.Models.Stations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airport.Dal
{
    public class AirportContext : DbContext
    {

        DbSet<LandingDTO> Landings { get; set; }
        DbSet<TakeoffDTO> Takeoffs { get; set; }
        DbSet<Plane> Planes { get; set; }
        DbSet<PlaneToStationMovementDTO> MovementHistory { get; set; }
   
        public AirportContext(DbContextOptions<AirportContext> options) : base(options)
        {
            //this.Database.EnsureDeleted();
            //this.Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}

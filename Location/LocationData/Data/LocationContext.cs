using LocationData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationData.Data
{
    public class LocationContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }

        public LocationContext(DbContextOptions<LocationContext> options) : base(options)
        {
        }
    }
}

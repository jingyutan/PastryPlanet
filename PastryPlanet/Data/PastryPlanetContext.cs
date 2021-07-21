using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PastryPlanet.Models;

namespace PastryPlanet.Data
{
    public class PastryPlanetContext : DbContext
    {
        public PastryPlanetContext (DbContextOptions<PastryPlanetContext> options)
            : base(options)
        {
        }

        public DbSet<PastryPlanet.Models.Product> Product { get; set; }
    }
}

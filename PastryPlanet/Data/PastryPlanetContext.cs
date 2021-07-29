using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PastryPlanet.Models;

namespace PastryPlanet.Data
{
    public class PastryPlanetContext : IdentityDbContext<ApplicationUser>
    {
        public PastryPlanetContext (DbContextOptions<PastryPlanetContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<PastryPlanet.Models.Product> Product { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<PastryPlanet.Models.AuditRecord> AuditRecords { get; set; }
    }
}

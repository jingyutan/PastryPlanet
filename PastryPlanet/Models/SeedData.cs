using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PastryPlanet.Data;

namespace PastryPlanet.Models
{
    public class SeedData
    {
        public static void Initialize (IServiceProvider serviceProvider)
        {
            using (var context = new PastryPlanetContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<PastryPlanetContext>>()))
            {
                // Look for any Products
                if (context.Product.Any())
                {
                    return; // DB has been seeded
                }
                
            }
        }
    }
}

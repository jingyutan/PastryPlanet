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
                context.Product.AddRange(
                    new Product
                    {
                        ID = 1,
                        Name = "Chewey Chocolate Chip Cookies",
                        Description = "Freshly baked, chewey cookies with pockets of flowey dark chocolate",
                        Price = 2.90,
                        QuantityInStock = 20
                    },

                    new Product
                    {
                        ID = 2,
                        Name = "Flakey Crossaints",
                        Description = "Perfectly laminated, buttery and decadent pastry.",
                        Price = 5.90,
                        QuantityInStock = 15
                    },

                    new Product
                    {
                        ID = 3,
                        Name = "Classic NY Cheesecake",
                        Description = "Indulgent and creamy with a crisp Graham Cracker crust.",
                        Price = 4.90,
                        QuantityInStock = 25
                    }
                );
                context.SaveChanges();
                    
                 
            }
        }
    }
}

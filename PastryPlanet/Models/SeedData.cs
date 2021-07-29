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
                        Name = "Chewey Chocolate Chip Cookies",
                        Description = "Freshly baked, chewey cookies with pockets of flowey dark chocolate",
                        Price = 2.90,
                        QuantityInStock = 20,
                        Image = "chewycookie1.jpg"
                    },

                    new Product
                    {
                        Name = "Flakey Crossaints",
                        Description = "Perfectly laminated, buttery and decadent pastry.",
                        Price = 5.90,
                        QuantityInStock = 15,
                        Image = "croissant1.jpg"
                    },

                    new Product
                    {
                        Name = "Classic NY Cheesecake",
                        Description = "Indulgent and creamy with a crisp Graham Cracker crust.",
                        Price = 4.90,
                        QuantityInStock = 25,
                        Image = "cheesecake1.jpg"
                    }
                );
                if (context.ProductAudit.Any())
                {
                    return; // DB has been seeded
                }
                context.ProductAudit.AddRange(
                    new ProductAudit
                    {
                        Name = "Chewey Chocolate Chip Cookies",
                        Description = "Freshly baked, chewey cookies with pockets of flowey dark chocolate",
                        Price = 2.90,
                        QuantityInStock = 20,
                        Image = "chewycookie1.jpg",
                        Productid = 1,
                        Username = "admin"
                    },

                    new ProductAudit
                    {
                        Name = "Flakey Crossaints",
                        Description = "Perfectly laminated, buttery and decadent pastry.",
                        Price = 5.90,
                        QuantityInStock = 15,
                        Image = "croissant1.jpg",
                        Productid = 2,
                        Username = "admin"
                    },

                    new ProductAudit
                    {
                        Name = "Classic NY Cheesecake",
                        Description = "Indulgent and creamy with a crisp Graham Cracker crust.",
                        Price = 4.90,
                        QuantityInStock = 25,
                        Image = "cheesecake1.jpg",
                        Productid = 3,
                        Username = "admin"
                    }
                );
                context.SaveChanges();
                    
                 
            }
        }
    }
}

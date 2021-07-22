using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PastryPlanet.Models
{
    public class Product
    {
// ------------------------------ Section for constructors ------------------------------
        public int ID { get; set; } // Primary key for products
        public string Name { get; set; } // Name of the product
        public string Description { get; set; } // Description of product
        public double Price { get; set; } // Price of the product
        public int QuantityInStock { get; set; } // Stock of the product
        public string Image { get; set; } // Image path of the product

        /*public string Category { get; set; }  Category of the product 
                                              (might not be needed, 
                                                comment out first)*/

    }
}

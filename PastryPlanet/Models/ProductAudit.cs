using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PastryPlanet.Models
{
    public class ProductAudit
    {
        public int ID { get; set; } // Primary key for products
        public string Name { get; set; } // Name of the product
        public string Description { get; set; } // Description of product
        public double Price { get; set; } // Price of the product
        [Display(Name = "Stock")]
        public int QuantityInStock { get; set; } // Stock of the product
        public string Image { get; set; } // Image path of the product
        public int Productid { get; set; } //product id
        [DataType(DataType.DateTime)]
        public DateTime DateTimeStamp { get; set; }
        [Display(Name = "Performed By")]
        public string Username { get; set; }
    }
}

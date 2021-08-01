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
        [RegularExpression("^[a-zA-Z]{1,50}*$", ErrorMessage ="Name can only have letters")]
        public string Name { get; set; } // Name of the product
        public string Description { get; set; } // Description of product

        [RegularExpression("^[0-9]{1,4}*$", ErrorMessage ="Price can only contain numbers")]
        public double Price { get; set; } // Price of the product

        [Display(Name="Stock")]
        [RegularExpression("^[0-9]{1,4}*$", ErrorMessage ="Stock can only contain numbers")]
        public int QuantityInStock { get; set; } // Stock of the product
        [RegularExpression("^[a-zA-Z0-9.]{1,20}*$", ErrorMessage ="Invalid image Name")]
        public string Image { get; set; } // Image path of the product


        
    }
}

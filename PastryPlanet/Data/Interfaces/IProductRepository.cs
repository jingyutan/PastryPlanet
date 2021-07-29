using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PastryPlanet.Models;


namespace PastryPlanet.Data.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; set; }
        IEnumerable<Product> PreferredProducts { get; set; }
        Product GetProductById(int productId);

    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PastryPlanet.Data;
using PastryPlanet.Models;

namespace PastryPlanet.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly Product.IProductRepository _productRepository;
        private readonly ShoppingCart _shoppingCart;

// ------------------- Constructor for controller ---------------------------
        public ShoppingCartController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}

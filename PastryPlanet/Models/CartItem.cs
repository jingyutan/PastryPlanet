﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PastryPlanet.Models
{
    public class CartItem
    {
        public int ID { get; set; }

        public int CartID { get; set; }

        public int ProductID { get; set; }

        public int Quantity { get; set; }

        // Navigational Properties
        public cart Cart { get; set; }

        public Product Product { get; set; }
    }
}

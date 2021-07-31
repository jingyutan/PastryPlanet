using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PastryPlanet.Models
{
    public class cart
    {
        public int ID { get; set; }
        [Required]
        public string UserID { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PastryPlanet.Models
{
    public class AuditRecord
    {
        [Key]
        public int Audit_ID { get; set; }

        [RegularExpression("^[a-zA-Z]{1,50}*$", ErrorMessage = "Action can only have letters")]
        [Display(Name = "Audit Action")]
        public string AuditActionType { get; set; }
        // Could be  Login Success /Failure/ Logout, Create, Delete, View, Update

        [RegularExpression("^[a-zA-Z0-9@.]{1,50}*$", ErrorMessage = "Invalid Email!")]
        [Display(Name = "Email of User")]
        public string Username { get; set; }
        //Logged in user performing the action

        [Display(Name = "Date/Time Stamp")]
        [DataType(DataType.DateTime)]
        public DateTime DateTimeStamp { get; set; }
        //Time when the event occurred

        [Display(Name = "Product ID")]
        public int KeyProductID { get; set; }
        //Store the ID of product that is affected
    }

}


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace BangazonAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BangazonAPI.Models
{
    public class Product
    {

        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int ProductTypeId { get; set; }

        [Required]
        public int CustomerId { get; set; }
    }
}

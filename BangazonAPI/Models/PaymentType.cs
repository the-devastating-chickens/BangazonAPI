using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BangazonAPI.Models
{
    public class PaymentType
    {
        public int Id { get; set; }

        [Required]
        public int AcctNumber { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
    }
}

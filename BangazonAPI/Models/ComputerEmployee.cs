using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BangazonAPI.Models
{
    public class ComputerEmployee
    {
        public int Id { get; set; }

        [Required]
        public DateTime AssignedDate { get; set; }

        [Required]
        public DateTime UnassignedDate { get; set; }

        [Required]
        public int EmployeeId { get; set; }

        [Required]
        public int ComputerId { get; set; }
    }
}

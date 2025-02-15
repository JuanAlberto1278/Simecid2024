using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Service: BaseDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "PhoneNumber is required")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Tax is required")]
        public double Tax { get; set; }

        public int BranchId { get; set; }
    }
}
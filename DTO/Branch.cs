using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Branch : BaseDTO
    {

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Adress is required")]
        public string Address { get; set; }

        public string Description { get; set; }

        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public double ServicePrice { get; set; }

        public double ServiceTax { get; set; }

    }
}

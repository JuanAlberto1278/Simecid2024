using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Diagnostic : BaseDTO
    {
        public int PatientId { get; set; }

        public string DiagnoseName { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class MedicalReport : BaseDTO
    {
        public int PatientId { get; set; }

        public string PatientName { get; set; }

        public string PatientLastName { get; set; }

        public string Historial {  get; set; }

        public string MedicalNotes { get; set; }

        public DateTime Date { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Prescription : BaseDTO
    {
        [Required(ErrorMessage = "Patient id is required")]
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        [Required(ErrorMessage = "Prescription is required")]
        public string PrescriptionName { get; set; }

        [Required(ErrorMessage = "Madication name id is required")]
        public string MedicationName { get; set; }
        public DateTime PrescriptionDate { get; set; }
        public string Recommendations { get; set; }
        public string UploadedFile { get; set; }
    }
}

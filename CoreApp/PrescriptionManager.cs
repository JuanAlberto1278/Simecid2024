using DataAccess.CRUD;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class PrescriptionManager
    {
        public void Create(Prescription prescription)
        {
            var pc = new PrescriptionCrudFactory();
            pc.Create(prescription);
        }

        public void Update(Prescription prescription)
        {
            var pc = new PrescriptionCrudFactory();
            pc.Update(prescription);
        }

        public void Delete(Prescription prescription)
        {
            var pc = new PrescriptionCrudFactory();
            pc.Delete(prescription);
        }

        public List<Prescription> RetrieveAll()
        {
            var pc = new PrescriptionCrudFactory();
            return pc.RetrieveAll<Prescription>();
        }

        public Prescription RetrieveById(int prescriptionId)
        {
            var pc = new PrescriptionCrudFactory();
            return pc.RetrieveById<Prescription>(prescriptionId);
        }

        public List<Prescription> RetrievePrescriptionByEmail(string userEmail)
        {
            var pc = new PrescriptionCrudFactory();
            return pc.RetrievePrescriptionByEmail<Prescription>(userEmail);
        }
    }
}

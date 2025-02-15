using DataAccess.CRUD;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class MedicalReportManager
    {

        public void Create(MedicalReport medicalReport)
        {
            // Se crea una instancia de el CrudFactory para interactuar con la capa de acceso a datos (DAO)
            var mr = new MedicalReportCrudFactory();

            // Aquí se realizan validaciones 


            //Aqui se ejecuta el metodo del crud
            mr.Create(medicalReport);
        }

        public void Update(MedicalReport medicalReport)
        {
            var mr = new MedicalReportCrudFactory();

            // Aquí se realizan validaciones 
            //

            //Aqui se ejecuta el metodo del crud
            mr.Update(medicalReport);
        }

        public void Delete(MedicalReport medicalReport)
        {
            var mr = new MedicalReportCrudFactory();

            // Aquí se realizan validaciones 


            //Aqui se ejecuta el metodo del crud
            mr.Delete(medicalReport);
        }

        public List<MedicalReport> RetrieveAll()
        {
            var mr = new MedicalReportCrudFactory();

            return mr.RetrieveAll<MedicalReport>();
        }

        public List<MedicalReport> RetrieveMedicalReportByEmail(string userEmail)
        {
            var pc = new MedicalReportCrudFactory();
            return pc.RetrieveMedicalReportByEmail<MedicalReport>(userEmail);
        }


    }
}

using DataAccess.CRUD;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class MedicalExamManager
    {
        public void Create(MedicalExam medicalExam)
        {
            // Se crea una instancia de el CrudFactory para interactuar con la capa de acceso a datos (DAO)
            var me = new MedicalExamCrudFactory();

            // Aquí se realizan validaciones 


            //Aqui se ejecuta el metodo del crud
            me.Create(medicalExam);
        }

        public void Update(MedicalExam medicalExam)
        {
            var me = new MedicalExamCrudFactory();

            // Aquí se realizan validaciones 
            //

            //Aqui se ejecuta el metodo del crud
            me.Update(medicalExam);
        }

        public void Delete(MedicalExam medicalExam)
        {
            var me = new MedicalExamCrudFactory();

            // Aquí se realizan validaciones 


            //Aqui se ejecuta el metodo del crud
            me.Delete(medicalExam);
        }

        public List<MedicalExam> RetrieveAll()
        {
            var me = new MedicalExamCrudFactory();

            return me.RetrieveAll<MedicalExam>();
        }

        public List<MedicalExam> RetrieveMedicalExamByEmail(string userEmail)
        {
            var pc = new MedicalExamCrudFactory();
            return pc.RetrieveMedicalExamByEmail<MedicalExam>(userEmail);
        }
    }
}
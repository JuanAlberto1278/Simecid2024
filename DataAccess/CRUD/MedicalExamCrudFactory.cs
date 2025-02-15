using DataAccess.DAOs;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class MedicalExamCrudFactory : CrudFactory
    {
        public MedicalExamCrudFactory()
        {
            _dao = SqlDao.GetInstace();

        }

        public override void Create(BaseDTO baseDTO)
        {
            // Convertir BaseDTO en un examen médico
            var exam = baseDTO as MedicalExam;

            // Crear la operación SQL para ejecutar el procedimiento almacenado
            var sqlOperation = new SqlOperation { ProcedureName = "CRE_MEDICAL_EXAM_PR" };
            sqlOperation.AddIntParam("P_PATIENT_ID", exam.PatientId);
            sqlOperation.AddVarcharParam("P_EXAM_TYPE", exam.Examtype);
            sqlOperation.AddDatetimeParam("P_EXAM_DATE", exam.ExamDate);
            sqlOperation.AddVarcharParam("P_DETAILS", exam.Details);
            sqlOperation.AddDoubleParam("P_WEIGHT", exam.Weight);
            sqlOperation.AddDoubleParam("P_SIZE", exam.Size);
            sqlOperation.AddVarcharParam("P_BODY_MASS", exam.BodyMass);
            sqlOperation.AddVarcharParam("P_RESULT", exam.Result);

            // Ejecutar el procedimiento almacenado
            _dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO baseDTO)
        {
            var exam = baseDTO as MedicalExam;

            // Verificar si el usuario tiene un nombre válido
            if (exam == null || exam.Id == 0)
            {
                throw new ArgumentException("Invalid Id.");
            }

            var SqlOperation = new SqlOperation { ProcedureName = "DEL_MEDICAL_EXAM_PR" };
            SqlOperation.AddIntParam("P_MEDICAL_EXAM_ID", exam.Id);

            _dao.ExecuteProcedure(SqlOperation);
        }

        public override void Update(BaseDTO baseDTO)
        {
            var exam = baseDTO as MedicalExam;

            // Crear la operación SQL para ejecutar el procedimiento almacenado
            var sqlOperation = new SqlOperation { ProcedureName = "UPD_MEDICAL_EXAM_PR" };
            sqlOperation.AddIntParam("P_MEDICAL_EXAM_ID", exam.Id);
            sqlOperation.AddIntParam("P_PATIENT_ID", exam.PatientId);
            sqlOperation.AddVarcharParam("P_EXAM_TYPE", exam.Examtype);
            sqlOperation.AddDatetimeParam("P_EXAM_DATE", exam.ExamDate);
            sqlOperation.AddVarcharParam("P_DETAILS", exam.Details);
            sqlOperation.AddDoubleParam("P_WEIGHT", exam.Weight);
            sqlOperation.AddDoubleParam("P_SIZE", exam.Size);
            sqlOperation.AddVarcharParam("P_BODY_MASS", exam.BodyMass);
            sqlOperation.AddVarcharParam("P_RESULT", exam.Result);

            // Ejecutar el procedimiento almacenado
            _dao.ExecuteProcedure(sqlOperation);
        }

        private MedicalExam BuildExam(Dictionary<string, object> row)
        {
            var ExamToReturn = new MedicalExam()
            {
                Id = (int)row["MEDICAL_EXAM_ID"],
                PatientId = (int)row["PATIENT_ID"],
                Examtype = (string)row["EXAM_TYPE"],
                ExamDate = (DateTime)row["EXAM_DATE"],
                Details = (string)row["DETAILS"],
                Weight = (double)row["WEIGHT"],
                Size = (double)row["SIZE"],
                BodyMass = (string)row["BODY_MASS"],
                Result = (string)row["RESULT"] 
            };

            return ExamToReturn;
        }

        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }

        //public override T RetrieveById<T>(int Id)
        //{
        //    var sqlOperation = new SqlOperation() { ProcedureName = "RET_USER_BY_ID" };
        //    sqlOperation.AddIntParam("P_USER_ID", Id);
        //    var lstResult = _dao.ExecuteQueryProcedure(sqlOperation);

        //    if (lstResult.Count > 0)
        //    {
        //        var row = lstResult[0]; // Extract the first row from the result
        //        var userFound = BuildUser(row);
        //        return (T)Convert.ChangeType(userFound, typeof(T));
        //    }
        //    return default(T); // Return default value for type T if user not found
        //}


        public override List<T> RetrieveAll<T>()
        {

            var examList = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_MEDICAL_EXAM_ALL" };
            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var exam = BuildExam(row);
                    examList.Add((T)Convert.ChangeType(exam, typeof(T)));
                }
            }
            return examList;
        }

        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> RetrieveMedicalExamByEmail<T>(string userEmail)
        {
            var lstAppts = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_MEDICAL_EXAM_BY_EMAIL" };
            sqlOperation.AddVarcharParam("P_USER_EMAIL", userEmail); // Corregido el nombre del parámetro

            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                // Recorre cada fila en la lista de resultados
                foreach (var row in lstResults)
                {
                    var appt = BuildExam(row);

                    // Esta conversión es necesaria porque la lista está definida como List<T>.
                    lstAppts.Add((T)Convert.ChangeType(appt, typeof(T)));
                }
            }

            // Retorna la lista final que contiene objetos del tipo T
            return lstAppts;
        }

    }
}
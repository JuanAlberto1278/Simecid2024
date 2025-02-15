using DataAccess.DAOs;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class PatientCrudFactory : CrudFactory
    {
        public PatientCrudFactory()
        {
            _dao = SqlDao.GetInstace();

        }

        public override void Create(BaseDTO baseDTO)
        {
            //Convertir BaseDTO en un usario
            var patient = baseDTO as Patient;
            //Creacion de instructivo para que el Dao pueda ejecutar
            var sqlOperation = new SqlOperation { ProcedureName = "CRE_PATIENT_PR" };
            sqlOperation.AddVarcharParam("P_NAME", patient.Name);
            sqlOperation.AddVarcharParam("P_LAST_NAME", patient.LastName);
            sqlOperation.AddIntParam("P_PHONE_NUMBER", patient.PhoneNumber);
            sqlOperation.AddVarcharParam("P_EMAIL", patient.Email);
            sqlOperation.AddVarcharParam("P_PASSWORD", patient.Password);
            sqlOperation.AddVarcharParam("P_SEX", patient.Sex);
            sqlOperation.AddDatetimeParam("P_BIRTHDATE", patient.BirthDate);
            sqlOperation.AddVarcharParam("P_ROLE", patient.Role);
            sqlOperation.AddVarcharParam("P_STATUS", patient.Status);


            _dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO baseDTO)
        {
            var patient = baseDTO as Patient;

            // Verificar si el usuario tiene un nombre válido
            if (patient == null || patient.Id == 0)
            {
                throw new ArgumentException("Invalid Id.");
            }

            var SqlOperation = new SqlOperation { ProcedureName = "DEL_PATIENT_PR" };
            SqlOperation.AddIntParam("P_PATIENT_ID", patient.Id);

            _dao.ExecuteProcedure(SqlOperation);
        }

        public override void Update(BaseDTO baseDTO)
        {
            var patient = baseDTO as Patient;


            if (patient == null || patient.Id == 0)
            {
                throw new ArgumentException("Invalid Id.");
            }


            var sqlOperation = new SqlOperation { ProcedureName = "UPD_PATIENT_PR" };
            sqlOperation.AddIntParam("P_PATIENT_ID", patient.Id);
            sqlOperation.AddVarcharParam("P_NAME", patient.Name);
            sqlOperation.AddVarcharParam("P_LAST_NAME", patient.LastName);
            sqlOperation.AddIntParam("P_PHONE_NUMBER", patient.PhoneNumber);
            sqlOperation.AddVarcharParam("P_EMAIL", patient.Email);
            sqlOperation.AddVarcharParam("P_PASSWORD", patient.Password);
            sqlOperation.AddVarcharParam("P_SEX", patient.Sex);
            sqlOperation.AddDatetimeParam("P_BIRTHDATE", patient.BirthDate);
            sqlOperation.AddVarcharParam("P_ROLE", patient.Role);
            sqlOperation.AddVarcharParam("P_STATUS", patient.Status);


            _dao.ExecuteProcedure(sqlOperation);
        }

        public List<T> RetrieveIdByEmail<T>(string Email)
        {
            var lstAppts = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_PATIENTID_BY_EMAIL" };
            sqlOperation.AddVarcharParam("@P_PATIENT_EMAIL", Email); // Corregido el nombre del parámetro

            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                // Recorre cada fila en la lista de resultados
                foreach (var row in lstResults)
                {
                    var appt = BuildIdByEmail(row);

                    // Esta conversión es necesaria porque la lista está definida como List<T>.
                    lstAppts.Add((T)Convert.ChangeType(appt, typeof(T)));
                }
            }

            // Retorna la lista final que contiene objetos del tipo T
            return lstAppts;
        }

        private Patient BuildPatient(Dictionary<string, object> row)
        {
            var patientToReturn = new Patient()
            {
                Id = (int)row["PATIENT_ID"],
                Name = (string)row["NAME"],
                LastName = (string)row["LAST_NAME"],
                PhoneNumber = (int)row["PHONE_NUMBER"],
                Email = (string)row["EMAIL"],
                Password = (string)row["PASSWORD"],
                Sex = (string)row["SEX"],
                BirthDate = (DateTime)row["BIRTHDATE"],
                Role = (string)row["ROLE"],
                Status = (string)row["STATUS"],
                Created = (DateTime)row["CREATED"],
                Province = (string)row["PROVINCE"],
                Address = (string)row["ADDRESS"]
            };
            return patientToReturn;
        }

        private Patient BuildIdByEmail(Dictionary<string, object> row)
        {
            var patientToReturn = new Patient()
            { 
                Id = (int)row["PATIENT_ID"]
                
            };
            return patientToReturn;
        }

        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }

        public override T RetrieveById<T>(int Id)
        {
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_PATIENT_BY_ID" };
            sqlOperation.AddIntParam("P_PATIENT_ID", Id);
            var lstResult = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResult.Count > 0)
            {
                var row = lstResult[0]; // Extract the first row from the result
                var patientFound = BuildPatient(row);
                return (T)Convert.ChangeType(patientFound, typeof(T));
            }
            return default(T); // Return default value for type T if user not found
        }

        public override List<T> RetrieveAll<T>()
        {

            var patientList = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_PATIENT_ALL" };
            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var patient = BuildPatient(row);
                    patientList.Add((T)Convert.ChangeType(patient, typeof(T)));
                }


            }
            return patientList;
        }



    }
}

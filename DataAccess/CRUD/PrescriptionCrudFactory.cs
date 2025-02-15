using DataAccess.DAOs;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class PrescriptionCrudFactory : CrudFactory
    {
        public PrescriptionCrudFactory()
        {
            _dao = SqlDao.GetInstace();
        }

        public override void Create(BaseDTO baseDTO)
        {
            var prescription = baseDTO as Prescription;

            // Crear la operación SQL para ejecutar el procedimiento almacenado
            var sqlOperation = new SqlOperation { ProcedureName = "CRE_PRESCRIPTION_PR" };
            sqlOperation.AddIntParam("P_PATIENT_ID", prescription.PatientId);
            sqlOperation.AddIntParam("P_DOCTOR_ID", prescription.DoctorId); // Opcional, puede ser NULL
            sqlOperation.AddVarcharParam("P_PRESCRIPTION_NAME", prescription.PrescriptionName);
            sqlOperation.AddVarcharParam("P_MEDICATION_NAME", prescription.MedicationName);
            sqlOperation.AddDatetimeParam("P_PRESCRIPTION_DATE", prescription.PrescriptionDate);
            sqlOperation.AddVarcharParam("P_RECOMMENDATIONS", prescription.Recommendations); // Opcional, puede ser NULL
            sqlOperation.AddVarcharParam("P_UPLOADED_FILE", prescription.UploadedFile); // Opcional, puede ser NULL

            // Ejecutar el procedimiento almacenado
            _dao.ExecuteProcedure(sqlOperation);
        }


        public override void Delete(BaseDTO baseDTO)
        {
            var prescription = baseDTO as Prescription;
            var sqlOperation = new SqlOperation { ProcedureName = "DEL_PRESCRIPTION_PR" };
            sqlOperation.AddIntParam("P_PRESCRIPTION_ID", prescription.Id);

            _dao.ExecuteProcedure(sqlOperation);
        }

        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var prescriptionList = new List<T>();
            var sqlOperation = new SqlOperation { ProcedureName = "RET_PRESCRIPTION_ALL" };
            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var prescription = BuildPrescription(row);
                    prescriptionList.Add((T)Convert.ChangeType(prescription, typeof(T)));
                }
            }

            return prescriptionList;
        }


        public override T RetrieveById<T>(int id)
        {
            var sqlOperation = new SqlOperation { ProcedureName = "RET_PRESCRIPTION_BY_ID" };
            sqlOperation.AddIntParam("P_PRESCRIPTION_ID", id);

            var prescriptionList = _dao.ExecuteQueryProcedure(sqlOperation);

            if (prescriptionList.Count > 0)
            {
                var row = prescriptionList[0];
                var prescriptionFound = BuildPrescription(row);
                return (T)Convert.ChangeType(prescriptionFound, typeof(T));
            }
            else
            {
                return default(T);
            }
        }

        private Prescription BuildPrescription(Dictionary<string, object> row)
        {
            var prescriptionToReturn = new Prescription()
            {
                Id = (int)row["PRESCRIPTION_ID"],
                PatientId = (int)row["PATIENT_ID"],
                DoctorId = (int)row["DOCTOR_ID"],
                PrescriptionName = (string)row["PRESCRIPTION_NAME"],
                MedicationName = (string)row["MEDICATION_NAME"],
                PrescriptionDate = (DateTime)row["PRESCRIPTION_DATE"],
                Recommendations = (string)row["RECOMMENDATIONS"],
                UploadedFile = (string)row["UPLOADED_FILE"]
            };

            return prescriptionToReturn;
        }

        public override void Update(BaseDTO baseDTO)
        {
            var prescription = baseDTO as Prescription;
            var sqlOperation = new SqlOperation { ProcedureName = "UPD_PRESCRIPTION_PR" };
            sqlOperation.AddIntParam("P_PRESCRIPTION_ID", prescription.Id);
            sqlOperation.AddIntParam("P_PATIENT_ID", prescription.PatientId);
            sqlOperation.AddIntParam("P_DOCTOR_ID", prescription.DoctorId);
            sqlOperation.AddVarcharParam("P_PRESCRIPTION_NAME", prescription.PrescriptionName);
            sqlOperation.AddVarcharParam("P_MEDICATION_NAME", prescription.MedicationName);
            sqlOperation.AddDatetimeParam("P_PRESCRIPTION_DATE", prescription.PrescriptionDate);
            sqlOperation.AddVarcharParam("P_RECOMMENDATIONS", prescription.Recommendations);
            sqlOperation.AddVarcharParam("P_UPLOADED_FILE", prescription.UploadedFile);

            _dao.ExecuteProcedure(sqlOperation);
        }

        public List<T> RetrievePrescriptionByEmail<T>(string userEmail)
        {
            var lstAppts = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_PRESCRIPTION_BY_EMAIL" };
            sqlOperation.AddVarcharParam("P_USER_EMAIL", userEmail); // Corregido el nombre del parámetro

            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                // Recorre cada fila en la lista de resultados
                foreach (var row in lstResults)
                {
                    var appt = BuildPrescription(row);

                    // Esta conversión es necesaria porque la lista está definida como List<T>.
                    lstAppts.Add((T)Convert.ChangeType(appt, typeof(T)));
                }
            }

            // Retorna la lista final que contiene objetos del tipo T
            return lstAppts;
        }
    }
}

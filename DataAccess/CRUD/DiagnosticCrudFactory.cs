using DataAccess.DAOs;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class DiagnosticCrudFactory : CrudFactory
    {

        public DiagnosticCrudFactory()
        {
            _dao = SqlDao.GetInstace();

        }

        public override void Create(BaseDTO baseDTO)
        {
            // Convertir BaseDTO en un examen médico
            var diagn = baseDTO as Diagnostic;

            // Crear la operación SQL para ejecutar el procedimiento almacenado
            var sqlOperation = new SqlOperation { ProcedureName = "CRE_DIAGNOSTIC_PR" };
            sqlOperation.AddIntParam("P_PATIENT_ID", diagn.PatientId);
            sqlOperation.AddVarcharParam("P_DIAGNOSE_NAME", diagn.DiagnoseName);
            sqlOperation.AddDatetimeParam("P_DATE", diagn.Date);
            sqlOperation.AddVarcharParam("P_DESCRIPTION", diagn.Description);

            // Ejecutar el procedimiento almacenado
            _dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO baseDTO)
        {
            var diagn = baseDTO as Diagnostic;

            // Verificar si el usuario tiene un nombre válido
            if (diagn == null || diagn.Id == 0)
            {
                throw new ArgumentException("Invalid Id.");
            }

            var SqlOperation = new SqlOperation { ProcedureName = "DEL_DIAGNOSTIC_PR" };
            SqlOperation.AddIntParam("P_DIAGNOSTIC_ID", diagn.Id);

            _dao.ExecuteProcedure(SqlOperation);
        }

        public override void Update(BaseDTO baseDTO)
        {
            var diagn = baseDTO as Diagnostic;

            // Crear la operación SQL para ejecutar el procedimiento almacenado
            var sqlOperation = new SqlOperation { ProcedureName = "UPD_DIAGNOSTIC_PR" };
            sqlOperation.AddIntParam("P_DIAGNOSTIC_ID", diagn.Id);
            sqlOperation.AddIntParam("P_PATIENT_ID", diagn.PatientId);
            sqlOperation.AddVarcharParam("P_DIAGNOSE_NAME", diagn.DiagnoseName);
            sqlOperation.AddDatetimeParam("P_DATE", diagn.Date);
            sqlOperation.AddVarcharParam("P_DESCRIPTION", diagn.Description);

            // Ejecutar el procedimiento almacenado
            _dao.ExecuteProcedure(sqlOperation);
        }

        private Diagnostic BuildDiagnostic(Dictionary<string, object> row)
        {
            var DiagnoseToReturn = new Diagnostic()
            {
                Id = (int)row["DIAGNOSTIC_ID"],
                PatientId = (int)row["PATIENT_ID"],
                DiagnoseName = (string)row["DIAGNOSE_NAME"],
                Date = (DateTime)row["DATE"],
                Description = (string)row["DESCRIPTION"]

            };

            return DiagnoseToReturn;
        }

        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {

            var diagnosticList = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_DIAGNOSTIC_ALL" };
            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var diagnose = BuildDiagnostic(row);
                    diagnosticList.Add((T)Convert.ChangeType(diagnose, typeof(T)));
                }


            }
            return diagnosticList;

        }

        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> RetrieveDiagnosticByEmail<T>(string userEmail)
        {
            var lstAppts = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_DIAGNOSTIC_BY_EMAIL" };
            sqlOperation.AddVarcharParam("P_USER_EMAIL", userEmail); // Corregido el nombre del parámetro

            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                // Recorre cada fila en la lista de resultados
                foreach (var row in lstResults)
                {
                    var appt = BuildDiagnostic(row);

                    // Esta conversión es necesaria porque la lista está definida como List<T>.
                    lstAppts.Add((T)Convert.ChangeType(appt, typeof(T)));
                }
            }

            // Retorna la lista final que contiene objetos del tipo T
            return lstAppts;
        }
    }
}

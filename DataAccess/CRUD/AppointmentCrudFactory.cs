using DataAccess.DAOs;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class AppointmentCrudFactory : CrudFactory
    {
        public AppointmentCrudFactory()
        {
            _dao = SqlDao.GetInstace();
        }
        public override void Create(BaseDTO baseDTO)
        {
            //throw new NotImplementedException();

            var appointment = baseDTO as Appointment;
            var sqlOperation = new SqlOperation { ProcedureName = "CRE_APPOINTMENT_PR" };
            sqlOperation.AddIntParam("P_PATIENT_ID", appointment.PatientId);
            sqlOperation.AddIntParam("P_DOCTOR_ID", appointment.DoctorId);
            sqlOperation.AddIntParam("P_SERVICE_ID", appointment.ServiceId);
            sqlOperation.AddIntParam("P_BRANCH_ID", appointment.BranchId);
            sqlOperation.AddDatetimeParam("P_APPOINTMENT_START_TIME", appointment.StartTime);
            sqlOperation.AddDatetimeParam("P_APPOINTMENT_END_TIME", appointment.EndTime);
            sqlOperation.AddVarcharParam("P_TEXT", appointment.Text);
            sqlOperation.AddVarcharParam("P_STATUS", appointment.Status);

            //ir al dao a ejecutar la operacion
            _dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseDTO baseDTO)
        {
            //throw new NotImplementedException();
            var appointment = baseDTO as Appointment;
            var sqlOperation = new SqlOperation { ProcedureName = "DEL_APPOINTMENT_PR" };
            sqlOperation.AddIntParam("P_APPOINTMENT_ID", appointment.Id);

            //ir al dao a ejecutar la operacion
            _dao.ExecuteProcedure(sqlOperation);
        }

        public override void Update(BaseDTO baseDTO)
        {
            //throw new NotImplementedException();
            var appointment = baseDTO as Appointment;
            var sqlOperation = new SqlOperation { ProcedureName = "UPD_APPOINTMENT_PR" };
            sqlOperation.AddIntParam("P_APPOINTMENT_ID", appointment.Id);
            sqlOperation.AddIntParam("P_PATIENT_ID", appointment.PatientId);
            sqlOperation.AddIntParam("P_DOCTOR_ID", appointment.DoctorId);
            sqlOperation.AddIntParam("P_SERVICE_ID", appointment.ServiceId);
            sqlOperation.AddIntParam("P_BRANCH_ID", appointment.BranchId);
            sqlOperation.AddDatetimeParam("P_APPOINTMENT_START_TIME", appointment.StartTime);
            sqlOperation.AddDatetimeParam("P_APPOINTMENT_END_TIME", appointment.EndTime);
            sqlOperation.AddVarcharParam("P_TEXT", appointment.Text);
            sqlOperation.AddVarcharParam("P_STATUS", appointment.Status);
            //ir al dao a ejecutar la operacion
            _dao.ExecuteProcedure(sqlOperation);
        }

        public List<T> RetrieveAppointmentsByEmail<T>(string userEmail)
        {
            var lstAppts = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_APPOINTMENTS_BY_EMAIL" };
            sqlOperation.AddVarcharParam("P_USER_EMAIL", userEmail); // Corregido el nombre del parámetro

            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                // Recorre cada fila en la lista de resultados
                foreach (var row in lstResults)
                {
                    var appt = BuildAppointment(row);

                    // Esta conversión es necesaria porque la lista está definida como List<T>.
                    lstAppts.Add((T)Convert.ChangeType(appt, typeof(T)));
                }
            }

            // Retorna la lista final que contiene objetos del tipo T
            return lstAppts;
        }

        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            //throw new NotImplementedException();
            var lstAppts = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_APPOINTMENT_ALL" };
            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                // Recorre cada fila en la lista de resultados
                foreach (var row in lstResults)
                {
                    var appt = BuildAppointment(row);

                    // Esta conversión es necesaria porque la lista está definida como List<T>.
                    lstAppts.Add((T)Convert.ChangeType(appt, typeof(T)));
                }
            }

            // Retorna la lista final que contiene objetos del tipo T
            return lstAppts;
        }




        public List<T> RetrieveAppointmentsByUserEmail<T>(string userEmail)
        {
            var lstAppts = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_APPOINTMENTS_BY_USER_EMAIL" };
            sqlOperation.AddVarcharParam("P_EMAIL", userEmail); // Corregido el nombre del parámetro

            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                // Recorre cada fila en la lista de resultados
                foreach (var row in lstResults)
                {
                    var appt = BuildAppointment(row);

                    // Esta conversión es necesaria porque la lista está definida como List<T>.
                    lstAppts.Add((T)Convert.ChangeType(appt, typeof(T)));
                }
            }

            // Retorna la lista final que contiene objetos del tipo T
            return lstAppts;
        }


        public override T RetrieveById<T>(int Id)
        {
            //throw new NotImplementedException();
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_APPOINTMENT_BY_ID" };
            sqlOperation.AddIntParam("P_APPOINTMENT_ID", Id);

            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            // Verifica si hay resultados en la lista
            if (lstResults.Count > 0)
            {
                // Obtiene la primera fila de la lista de resultados
                var row = lstResults[0];
                var appt = BuildAppointment(row);

                // Retorna el objeto  construido convertido al tipo genérico T
                return (T)Convert.ChangeType(appt, typeof(T));
            }

            // Si no hay resultados, retorna T
            return default(T);
        }

        private object BuildAppointment(Dictionary<string, object> row)
        {
            var apptToReturn = new Appointment()
            {
                Id = (int)row["APPOINTMENT_ID"],
                PatientId = (int)row["PATIENT_ID"],
                PatientName = (string)row["PATIENT_NAME"],
                PatientLastName = (string)row["PATIENT_LASTNAME"],
                DoctorId = (int)row["DOCTOR_ID"],
                DoctorName = (string)row["DOCTOR_NAME"],
                DoctorLastName = (string)row["DOCTOR_LASTNAME"],
                ServiceId = (int)row["SERVICE_ID"],
                ServiceName = (string)row["SERVICE_NAME"],
                BranchId = (int)row["BRANCH_ID"],
                BranchName = (string)row["BRANCH_NAME"],
                Text = (string)row["TEXT"],
                Status = (string)row["STATUS"],
                StartTime = (DateTime)row["APPOINTMENT_START_TIME"],
                EndTime = (DateTime)row["APPOINTMENT_END_TIME"]
            };
            return apptToReturn;
        }

    }
}
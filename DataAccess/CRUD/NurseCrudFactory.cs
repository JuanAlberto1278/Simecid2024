using DataAccess.DAOs;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
	public class NurseCrudFactory : CrudFactory
	{
		public NurseCrudFactory()
		{
			_dao = SqlDao.GetInstace();
		}

		public override void Create(BaseDTO baseDTO)
		{
			//Convertir BaseDTO en un usario
			var nurse = baseDTO as Nurse;
			//Creacion de instructivo para que el Dao pueda ejecutar
			var sqlOperation = new SqlOperation { ProcedureName = "CRE_NURSE_PR" };
			sqlOperation.AddIntParam("P_BRANCH_ID", nurse.BranchId);
			sqlOperation.AddVarcharParam("P_NAME", nurse.Name);
			sqlOperation.AddVarcharParam("P_LAST_NAME", nurse.LastName);
			sqlOperation.AddIntParam("P_PHONE_NUMBER", nurse.PhoneNumber);
			sqlOperation.AddVarcharParam("P_EMAIL", nurse.Email);
			sqlOperation.AddVarcharParam("P_PASSWORD", nurse.Password);
			sqlOperation.AddVarcharParam("P_SEX", nurse.Sex);
			sqlOperation.AddDatetimeParam("P_BIRTHDATE", nurse.BirthDate);
			sqlOperation.AddVarcharParam("P_ROLE", nurse.Role);
			sqlOperation.AddVarcharParam("P_STATUS", nurse.Status);


			_dao.ExecuteProcedure(sqlOperation);
		}

		public override void Delete(BaseDTO baseDTO)
		{
			var nurse = baseDTO as Nurse;

			// Verificar si el usuario tiene un nombre válido
			if (nurse == null || nurse.Id == 0)
			{
				throw new ArgumentException("Invalid Id.");
			}

			var SqlOperation = new SqlOperation { ProcedureName = "DEL_NURSE_PR" };
			SqlOperation.AddIntParam("P_NURSE_ID", nurse.Id);

			_dao.ExecuteProcedure(SqlOperation);
		}

		public override void Update(BaseDTO baseDTO)
		{
			var nurse = baseDTO as Nurse;


			if (nurse == null || nurse.Id == 0)
			{
				throw new ArgumentException("Invalid Id.");
			}


			var sqlOperation = new SqlOperation { ProcedureName = "UPD_NURSE_PR" };
			sqlOperation.AddIntParam("P_NURSE_ID", nurse.Id);
			sqlOperation.AddIntParam("P_BRANCH_ID", nurse.BranchId);
			sqlOperation.AddVarcharParam("P_NAME", nurse.Name);
			sqlOperation.AddVarcharParam("P_LAST_NAME", nurse.LastName);
			sqlOperation.AddIntParam("P_PHONE_NUMBER", nurse.PhoneNumber);
			sqlOperation.AddVarcharParam("P_EMAIL", nurse.Email);
			sqlOperation.AddVarcharParam("P_PASSWORD", nurse.Password);
			sqlOperation.AddVarcharParam("P_SEX", nurse.Sex);
			sqlOperation.AddDatetimeParam("P_BIRTHDATE", nurse.BirthDate);
			sqlOperation.AddVarcharParam("P_ROLE", nurse.Role);
			sqlOperation.AddVarcharParam("P_STATUS", nurse.Status);


			_dao.ExecuteProcedure(sqlOperation);
		}


		private Nurse BuildUser(Dictionary<string, object> row)
		{
			var NurseToReturn = new Nurse()
			{
				Id = (int)row["NURSE_ID"],
				BranchId = (int)row["BRANCH_ID"],
				Name = (string)row["NAME"],
				LastName = (string)row["LAST_NAME"],
				PhoneNumber = (int)row["PHONE_NUMBER"],
				Email = (string)row["EMAIL"],
				Password = (string)row["PASSWORD"],
				Sex = (string)row["SEX"],
				BirthDate = (DateTime)row["BIRTHDATE"],
				Role = (string)row["ROLE"],
				Status = (string)row["STATUS"],
				Created = (DateTime)row["CREATED"]
			};
			return NurseToReturn;
		}

		public override T Retrieve<T>()
		{
			throw new NotImplementedException();
		}

		public override T RetrieveById<T>(int Id)
		{
			var sqlOperation = new SqlOperation() { ProcedureName = "RET_NURSE_BY_ID" };
			sqlOperation.AddIntParam("P_NURSE_ID", Id);
			var lstResult = _dao.ExecuteQueryProcedure(sqlOperation);

			if (lstResult.Count > 0)
			{
				var row = lstResult[0]; // Extract the first row from the result
				var nurseFound = BuildUser(row);
				return (T)Convert.ChangeType(nurseFound, typeof(T));
			}
			return default(T); // Return default value for type T if user not found
		}

        public void AddSchedule(Nurse nurse)
        {
            var sqlOperation = new SqlOperation { ProcedureName = "ADD_SCHEDULE_NURSE_PR" };
            sqlOperation.AddIntParam("NURSE_ID", nurse.Id);
            sqlOperation.AddVarcharParam("SCHEDULE", nurse.Schedule);
            _dao.ExecuteProcedure(sqlOperation);
        }


        public override List<T> RetrieveAll<T>()
		{

			var nurseList = new List<T>();
			var sqlOperation = new SqlOperation() { ProcedureName = "RET_NURSE_ALL" };
			var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

			if (lstResults.Count > 0)
			{
				foreach (var row in lstResults)
				{
					var nurse = BuildUser(row);
					nurseList.Add((T)Convert.ChangeType(nurse, typeof(T)));
				}

			}
			return nurseList;
		}
	}
}

using DataAccess.DAOs;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class BranchCrudFactory : CrudFactory
    {

        public BranchCrudFactory()
        {
            _dao = SqlDao.GetInstace();

        }

        public override void Create(BaseDTO baseDTO)
        {
            // Convertir BaseDTO en una sede
            var branch = baseDTO as Branch;

            // Creación de instructivo para que el Dao pueda ejecutar
            var sqlOperation = new SqlOperation { ProcedureName = "CRE_BRANCH_PR" };
            sqlOperation.AddVarcharParam("P_NAME", branch.Name);
            sqlOperation.AddVarcharParam("P_ADDRESS", branch.Address);
            sqlOperation.AddVarcharParam("P_DESCRIPTION", branch.Description);

            // Quemar valores para ServiceId, ServiceName, ServicePrice y ServiceTax
            sqlOperation.AddIntParam("P_SERVICE_ID", 0); // Valor quemado para ServiceId
            sqlOperation.AddVarcharParam("P_SERVICE_NAME", "Default"); // Valor quemado para ServiceName
            sqlOperation.AddDoubleParam("P_SERVICE_PRICE", 0); // Valor quemado para ServicePrice
            sqlOperation.AddDoubleParam("P_SERVICE_TAX", 0); // Valor quemado para ServiceTax

            _dao.ExecuteProcedure(sqlOperation);
        }


        public override void Delete(BaseDTO baseDTO)
        {
            var branch = baseDTO as Branch;

            // Verificar si el branch tiene un nombre válido
            if (branch == null || branch.Id == 0)
            {
                throw new ArgumentException("Invalid Id.");
            }

            var SqlOperation = new SqlOperation { ProcedureName = "DEL_BRANCH_PR" };
            SqlOperation.AddIntParam("P_BRANCH_ID", branch.Id);

            _dao.ExecuteProcedure(SqlOperation);
        }

        public override void Update(BaseDTO baseDTO)
        {
            var branch = baseDTO as Branch;


            if (branch == null || branch.Id == 0)
            {
                throw new ArgumentException("Invalid Id.");
            }


            var sqlOperation = new SqlOperation { ProcedureName = "UPD_BRANCH_PR" };
            sqlOperation.AddIntParam("P_BRANCH_ID", branch.Id);
            sqlOperation.AddVarcharParam("P_NAME", branch.Name);
            sqlOperation.AddVarcharParam("P_ADDRESS", branch.Address);
            sqlOperation.AddVarcharParam("P_DESCRIPTION", branch.Description);


            _dao.ExecuteProcedure(sqlOperation);
        }


        private Branch BuildBranch(Dictionary<string, object> row)
        {
            var branchToReturn = new Branch()
            {
                Id = (int)row["BRANCH_ID"],
                Name = (string)row["NAME"],
                Address = (string)row["ADDRESS"],
                Description = (string)row["DESCRIPTION"],
                //ServiceId = (int)row["SERVICE_ID"],
                //ServiceName = (string)row["SERVICE_NAME"]

            };
            return branchToReturn;
        }

        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }

        public override T RetrieveById<T>(int Id)
        {
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_BRANCH_BY_ID" };
            sqlOperation.AddIntParam("P_BRANCH_ID", Id);
            var lstResult = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResult.Count > 0)
            {
                var row = lstResult[0]; // Extract the first row from the result
                var branchFound = BuildBranch(row);
                return (T)Convert.ChangeType(branchFound, typeof(T));
            }
            return default(T); // Return default value for type T if user not found
        }


        public override List<T> RetrieveAll<T>()
        {

            var branchList = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_BRANCH_ALL" };
            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var branch = BuildBranch(row);
                    branchList.Add((T)Convert.ChangeType(branch, typeof(T)));
                }


            }
            return branchList;
        }


       public void AddServiceToBranch(Branch branch)
        {
            var sqlOperation = new SqlOperation { ProcedureName = "ADD_SERVICES_TO_BRANCH" };
            sqlOperation.AddIntParam("P_BRANCH_ID", branch.Id);
            sqlOperation.AddIntParam("P_SERVICE_ID", branch.ServiceId);
            _dao.ExecuteProcedure(sqlOperation);
        }

        private Branch BuildBranchXServices(Dictionary<string, object> row)
        {
            var branchToReturn = new Branch()
            {
                Id = (int)row["BRANCH_ID"],
                Name = (string)row["BRANCH_NAME"],
                ServiceId = (int)row["SERVICE_ID"],
                ServiceName = (string)row["SERVICE_NAME"],
                ServicePrice = (double)row["PRICE"],
                ServiceTax = (double)row["TAX"]
            };
            return branchToReturn;
        }
        public List<T> RetrieveServicesByBranchId<T>(int Id)
        {
            var sqlOperation = new SqlOperation() { ProcedureName = "GET_SERVICES_BY_BRANCH_ID_PR" };
            sqlOperation.AddIntParam("P_BRANCH_ID", Id);
            var lstResult = _dao.ExecuteQueryProcedure(sqlOperation);

            List<T> servicesList = new List<T>(); // Lista para almacenar los servicios

            foreach (var row in lstResult)
            {
                var service = BuildBranchXServices(row);
                servicesList.Add((T)Convert.ChangeType(service, typeof(T)));
            }

            return servicesList;
        }

        public  List<T> RetrieveAllBranchServices<T>()
        {

            var branchList = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "RETRIEVE_ALL_BRANCH_SERVICES" };
            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var branch = BuildBranchXServices(row);
                    branchList.Add((T)Convert.ChangeType(branch, typeof(T)));
                }


            }
            return branchList;
        }
    }
}

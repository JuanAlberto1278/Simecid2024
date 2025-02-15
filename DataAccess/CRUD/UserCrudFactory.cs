using DataAccess.DAOs;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class UserCrudFactory : CrudFactory
    {
        public UserCrudFactory()
        {
            _dao = SqlDao.GetInstace();

        }

        public override void Create(BaseDTO baseDTO)
        {
            var user = baseDTO as User;

            // Crear la operación SQL para ejecutar el procedimiento almacenado
            var sqlOperation = new SqlOperation { ProcedureName = "CRE_USER_PR" };
            sqlOperation.AddVarcharParam("P_NAME", user.Name);
            sqlOperation.AddVarcharParam("P_LAST_NAME", user.LastName);
            sqlOperation.AddIntParam("P_PHONE_NUMBER", user.PhoneNumber);
            sqlOperation.AddVarcharParam("P_EMAIL", user.Email);
            sqlOperation.AddVarcharParam("P_PASSWORD", user.Password);
            sqlOperation.AddVarcharParam("P_SEX", user.Sex);
            sqlOperation.AddDatetimeParam("P_BIRTHDATE", user.BirthDate);
            sqlOperation.AddVarcharParam("P_ROLE", user.Role);
            sqlOperation.AddVarcharParam("P_STATUS", user.Status);
            sqlOperation.AddVarcharParam("P_PROVINCE", user.Province);
            sqlOperation.AddVarcharParam("P_ADDRESS", user.Address);
            sqlOperation.AddVarcharParam("P_IMAGE_NAME", user.ImageName);

            // Ejecutar el procedimiento almacenado
            _dao.ExecuteProcedure(sqlOperation);
        }



        public override void Delete(BaseDTO baseDTO)
        {
            var user = baseDTO as User;

            // Verificar si el usuario tiene un nombre válido
            if (user == null || user.Id == 0)
            {
                throw new ArgumentException("Invalid Id.");
            }

            var SqlOperation = new SqlOperation { ProcedureName = "DEL_USER_PR" };
            SqlOperation.AddIntParam("P_USER_ID", user.Id);

            _dao.ExecuteProcedure(SqlOperation);
        }

        public  void DeleteUserData(BaseDTO baseDTO)
        {
            var user = baseDTO as UserUpdData;

            // Verificar si el usuario tiene un nombre válido
            if (user == null || user.Id == 0)
            {
                throw new ArgumentException("Invalid Id.");
            }

            var SqlOperation = new SqlOperation { ProcedureName = "DEL_USER_PR" };
            SqlOperation.AddIntParam("P_USER_ID", user.Id);

            _dao.ExecuteProcedure(SqlOperation);
        }

        public override void Update(BaseDTO baseDTO)
        {
            var user = baseDTO as User;

            // Crear la operación SQL para ejecutar el procedimiento almacenado
            var sqlOperation = new SqlOperation { ProcedureName = "UPD_USER_PR" };
            sqlOperation.AddIntParam("P_USER_ID", user.Id);
            sqlOperation.AddVarcharParam("P_NAME", user.Name);
            sqlOperation.AddVarcharParam("P_LAST_NAME", user.LastName);
            sqlOperation.AddIntParam("P_PHONE_NUMBER", user.PhoneNumber);
            sqlOperation.AddVarcharParam("P_EMAIL", user.Email);
            sqlOperation.AddVarcharParam("P_PASSWORD", user.Password);
            sqlOperation.AddVarcharParam("P_SEX", user.Sex);
            sqlOperation.AddDatetimeParam("P_BIRTHDATE", user.BirthDate);
            sqlOperation.AddVarcharParam("P_ROLE", user.Role);
            sqlOperation.AddVarcharParam("P_STATUS", user.Status);
            sqlOperation.AddVarcharParam("P_PROVINCE", user.Province);
            sqlOperation.AddVarcharParam("P_ADDRESS", user.Address);
            sqlOperation.AddVarcharParam("P_IMAGE_NAME", user.ImageName);


            // Ejecutar el procedimiento almacenado
            _dao.ExecuteProcedure(sqlOperation);
        }

        public  void UpdateUserData(BaseDTO baseDTO)
        {
            var user = baseDTO as UserDataList;

            // Crear la operación SQL para ejecutar el procedimiento almacenado
            var sqlOperation = new SqlOperation { ProcedureName = "UPD_USER_DATA_SP" };
            sqlOperation.AddIntParam("P_USER_ID", user.Id);
            sqlOperation.AddVarcharParam("P_NAME", user.Name);
            sqlOperation.AddVarcharParam("P_LAST_NAME", user.LastName);
            sqlOperation.AddIntParam("P_PHONE_NUMBER", user.PhoneNumber);
            sqlOperation.AddVarcharParam("P_EMAIL", user.Email);
            sqlOperation.AddVarcharParam("P_ROLE", user.Role);
            sqlOperation.AddVarcharParam("P_PROVINCE", user.Province);
            sqlOperation.AddVarcharParam("P_ADDRESS", user.Address);
            

            // Ejecutar el procedimiento almacenado
            _dao.ExecuteProcedure(sqlOperation);
        }

        public void UpdateEmployeeData(BaseDTO baseDTO)
        {
            var user = baseDTO as Employee;

            // Crear la operación SQL para ejecutar el procedimiento almacenado
            var sqlOperation = new SqlOperation { ProcedureName = "UPD_EMPLOYEE_DATA_SP" };
            sqlOperation.AddIntParam("P_USER_ID", user.Id);
            sqlOperation.AddVarcharParam("P_NAME", user.Name);
            sqlOperation.AddVarcharParam("P_LAST_NAME", user.LastName);
            sqlOperation.AddVarcharParam("P_EMAIL", user.Email);
            sqlOperation.AddVarcharParam("P_ROLE", user.Role);
            sqlOperation.AddIntParam("P_BRANCH_ID", user.BranchId);
            sqlOperation.AddVarcharParam("P_SCHEDULE", user.Schedule);


            // Ejecutar el procedimiento almacenado
            _dao.ExecuteProcedure(sqlOperation);
        }

        public void UpdateUserRole(BaseDTO baseDTO)
        {
            var user = baseDTO as User;
            var sqlOperation = new SqlOperation { ProcedureName = "UPD_USER_ROL_PR" };
            sqlOperation.AddIntParam("UserID", user.Id);
            sqlOperation.AddVarcharParam("NewRole", user.Role);

            _dao.ExecuteProcedure(sqlOperation);
        }

        private User BuildUser(Dictionary<string, object> row)
        {
            var userToReturn = new User()
            {
                Id = (int)row["USER_ID"],
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
                Address = (string)row["ADDRESS"],
                ImageName = (string)row["IMAGE_NAME"]


            };
            return userToReturn;
        }

        private UserUpdData BuildUserData(Dictionary<string, object> row)
        {
            var userToReturn = new UserUpdData()
            {
                Id = (int)row["USER_ID"],
                Name = (string)row["NAME"],
                LastName = (string)row["LAST_NAME"],
                PhoneNumber = (int)row["PHONE_NUMBER"],
                Email = (string)row["EMAIL"],
                Role = (string)row["ROLE"],
                Province = (string)row["PROVINCE"],
                Address = (string)row["ADDRESS"],
                BranchId = (int)row["BRANCH_ID"],
                Schedule = (string)row["SCHEDULE"]
            };
            return userToReturn;
        }

        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }

        public override T RetrieveById<T>(int Id)
        {
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_USER_BY_ID" };
            sqlOperation.AddIntParam("P_USER_ID", Id);
            var lstResult = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResult.Count > 0)
            {
                var row = lstResult[0]; // Extract the first row from the result
                var userFound = BuildUser(row);
                return (T)Convert.ChangeType(userFound, typeof(T));
            }
            return default(T); // Return default value for type T if user not found
        }

        public T RetrieveRoleByUserEmail<T>(string email)
        {
            var sqlOperation = new SqlOperation() { ProcedureName = "RETRIEVE_ROL_BY_USER_EMAIL_SP" };
            sqlOperation.AddVarcharParam("EMAIL", email);
            var lstResult = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResult.Count > 0)
            {
                var row = lstResult[0]; // Extract the first row from the result
                var userFound = BuildUserRol(row);
                return (T)Convert.ChangeType(userFound, typeof(T));
            }
            return default(T); // Return default value for type T if user not found
        }

        public List<UserUpdData> RetrieveUsersByRole(string role)
        {
            var userListRole = new List<UserUpdData>();

            var sqlOperation = new SqlOperation() { ProcedureName = "RET_USER_BY_ROLE" };
            sqlOperation.AddVarcharParam("P_ROLE", role);
            var lstResult = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResult.Count > 0)
            {
                foreach (var row in lstResult)
                {
                    var user = BuildUserData(row);
                    userListRole.Add(user);
                }
            }

            return userListRole;
        }

        private object BuildUserRol(Dictionary<string, object> row)
        {
            var userToReturn = new User()
            {
                Role = (string)row["ROLE"],
            };
            return userToReturn;
        }

        public T UserByEmailAndPassword<T>(string email, string password)
        {

            var sqlOperation = new SqlOperation() { ProcedureName = "GET_USER_BY_EMAIL_AND_PASSWORD" };
            sqlOperation.AddVarcharParam("@P_EMAIL", email);
            sqlOperation.AddVarcharParam("@P_PASSWORD", password);

            var lstResult = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResult.Count > 0)
            {
                var row = lstResult[0]; 
                var userFound = BuildUser(row);
                return (T)Convert.ChangeType(userFound, typeof(T));
            }
            return default(T);
        }


        public override List<T> RetrieveAll<T>()
        {

            var userList = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_USER_ALL" };
            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var user = BuildUser(row);
                    userList.Add((T)Convert.ChangeType(user, typeof(T)));
                }


            }
            return userList;

        }

        public  List<T> RetrieveAllRoleUser<T>()
        {

            var userList = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_ALL_USERS_WITH_USER_ROLE" };
            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var user = BuildUser(row);
                    userList.Add((T)Convert.ChangeType(user, typeof(T)));
                }


            }
            return userList;

        }



        public List<T> GetStoredPasswordByEmail<T>(string email)
        {
            var passwordList = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "GET_PASSWORD_BY_EMAIL_PR" };
            sqlOperation.AddVarcharParam("P_EMAIL", email);
            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var password = BuildPassword(row);
                    passwordList.Add((T)Convert.ChangeType(password, typeof(T)));
                }
            }
            return passwordList;
        }

        private string BuildPassword(Dictionary<string, object> row)
        {
            return (string)row["PASSWORD"];
        }


        public bool AuthenticateUser(string email, string password)
        {
            var sqlOperation = new SqlOperation() { ProcedureName = "VAL_LOGIN_PR" };
            sqlOperation.AddVarcharParam("P_EMAIL", email);
            sqlOperation.AddVarcharParam("P_PASSWORD", password);

            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                return true;// Se encontraron resultados, el usuario está autenticado
            }
            else
            {
                return false;// No se encontraron resultados, la autenticación falló
            }
        }

        private User BuildLogin(Dictionary<string, object> row)
        {
            var userToReturn = new User()
            {
                Email = (string)row["EMAIL"],
                Password = (string)row["PASSWORD"],
            };
            return userToReturn;
        }

        public void UpdateUserPassword(string userEmail, string newPassword)
        {
            var sqlOperation = new SqlOperation { ProcedureName = "UPD_USER_PASSWORD" };
            sqlOperation.AddVarcharParam("EMAIL", userEmail);
            sqlOperation.AddVarcharParam("NEW_PASSWORD", newPassword);

            _dao.ExecuteProcedure(sqlOperation);
        }

        public T RetrieveUserByEmail<T>(string email)
        {
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_USER_BY_EMAIL" };
            sqlOperation.AddVarcharParam("P_EMAIL", email);
            var lstResult = _dao.ExecuteQueryProcedure(sqlOperation);
            if (lstResult.Count > 0)
            {
                var row = lstResult[0]; // Extract the first row from the result
                var userFound = BuildUser(row);
                return (T)Convert.ChangeType(userFound, typeof(T));
            }
            return default(T); // Return default value for type T if user not found
        }
    }
}

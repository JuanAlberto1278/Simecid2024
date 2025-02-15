using DataAccess.DAOs;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.CRUD
{
    public class ValidateOTPCrudFactory: CrudFactory
    {
        public ValidateOTPCrudFactory()
        {
            _dao = SqlDao.GetInstace();
        }

        public override void Create(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }

        public override void Delete(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>()
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override T RetrieveById<T>(int id)
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseDTO baseDTO)
        {
            throw new NotImplementedException();
        }

        public void CreateOTP(ValidateOTP validateOTP)
        {
            var vlOtp = validateOTP;

            var sqlOperation = new SqlOperation { ProcedureName = "CRE_VAL_OTP_PR" };
            sqlOperation.AddVarcharParam("P_EMAIL", vlOtp.Email);
            sqlOperation.AddVarcharParam("P_OTP", vlOtp.OTP);

            _dao.ExecuteProcedure(sqlOperation);
        }

        public ValidateOTP GetUserOTP(string email, string otp)
        {
            var sqlOperation = new SqlOperation() { ProcedureName = "GET_USER_OTP_PR" }; 
            sqlOperation.AddVarcharParam("P_EMAIL", email);
            sqlOperation.AddVarcharParam("P_OTP", otp);

            var result = _dao.ExecuteQueryProcedure(sqlOperation);
            var validateOTP = BuildOTP(result[0]);
            return validateOTP;
        }

        public List<T> RetrieveAllOTP<T>()
        {
            var otpList = new List<T>();
            var sqlOperation = new SqlOperation() { ProcedureName = "RET_OTP_ALL" };
            var lstResults = _dao.ExecuteQueryProcedure(sqlOperation);

            if (lstResults.Count > 0)
            {
                foreach (var row in lstResults)
                {
                    var user = BuildOTP(row);
                    otpList.Add((T)Convert.ChangeType(user, typeof(T)));
                }
            }
            return otpList;
        }



        private ValidateOTP BuildOTP(Dictionary<string, object> row)
        {
            var vldOTP = new ValidateOTP()
            {
                Email = (string)row["EMAIL"],
                OTP = (string)row["OTP"],
            };
            return vldOTP;
        }
    }
}

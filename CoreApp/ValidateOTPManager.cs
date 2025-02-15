using DataAccess.CRUD;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class ValidateOTPManager
    {
        public void CreateOTP(ValidateOTP validateOTP)
        {
            var vc = new ValidateOTPCrudFactory();
            if (validateOTP.Email == null || validateOTP.OTP == null)
            {
                throw new ArgumentNullException("Email or OTP cannot be null or empty.");
            }
            vc.CreateOTP(validateOTP);
        }

        public bool ValidateOTP(ValidateOTP validateOTP)
        {
            var vc = new ValidateOTPCrudFactory();
            if (validateOTP.Email == null || validateOTP.OTP == null)
            {
                throw new ArgumentNullException("Email or OTP cannot be null or empty.");
            }

            var response = vc.GetUserOTP(validateOTP.Email, validateOTP.OTP);
            if(response == null) 
            {
                throw new ArgumentNullException("Incorrect data");
            }
            return true;
        }

        public List<ValidateOTP> RetrieveAllOTP()
        {
            var uc = new ValidateOTPCrudFactory();
            return uc.RetrieveAllOTP<ValidateOTP>();
        }
    }
}

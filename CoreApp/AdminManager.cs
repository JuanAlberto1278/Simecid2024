using DataAccess.CRUD;
using DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CoreApp
{
    public class AdminManager
    {
        //Metodo para create


        public void Create(Admin admin)
        {
            var ac = new AdminCrudFactory();

            //Valiacion de forma

            if (!IsValidName(admin.Name))
            {
                throw new Exception("Invalid name format");
            }
            else if (!IsValidLastName(admin.LastName))
            {
                throw new Exception("Invalid Lastname format");
            }
            else if (!IsValidPhoneNumber(admin.PhoneNumber))
            {
                throw new Exception("Invalid phone number format");
            }
            else if (!IsValidEmail(admin.Email))
            {
                throw new Exception("Email is required");
            }
            else if (!IsValidPassword(admin.Password))
            {
                throw new Exception("Invalid Password format");
            }
            else if (!IsValidSex(admin.Sex))
            {
                throw new Exception("Invalid Sex format");
            }
            else if (!IsValidBirthDate(admin.BirthDate))
            {
                throw new Exception("Invalid birth date format");
            }
            else if (!IsValidRole(admin.Role))
            {
                throw new Exception("Invalid role format");
            }
            else if (!IsValidStatus(admin.Status))
            {
                throw new Exception("Invalid status value");
            }
            ac.Create(admin);

        }
        public List<string> GetSpecialtiesByBranch(int branchId)
        {
            var ac = new AdminCrudFactory();
            return ac.GetSpecialtiesByBranch(branchId);
        }

        public List<Admin> RetrieveAll()
        {
            var ac = new AdminCrudFactory();
            return ac.RetrieveAll<Admin>();
        }

        public Admin RetrieveById(int adminId)
        {
            var ac = new AdminCrudFactory();
            return ac.RetrieveById<Admin>(adminId);
        }


        public void Update(Admin admin)
        {
            var ac = new AdminCrudFactory();

            if (!IsValidName(admin.Name))
            {
                throw new Exception("Invalid name format");
            }
            else if (!IsValidLastName(admin.LastName))
            {
                throw new Exception("Invalid Lastname format");
            }
            else if (!IsValidPhoneNumber(admin.PhoneNumber))
            {
                throw new Exception("Invalid phone number format");
            }
            else if (!IsValidEmail(admin.Email))
            {
                throw new Exception("Email is required");
            }
            else if (!IsValidPassword(admin.Password))
            {
                throw new Exception("Invalid Password format");
            }
            else if (!IsValidSex(admin.Sex))
            {
                throw new Exception("Invalid Sex format");
            }
            else if (!IsValidBirthDate(admin.BirthDate))
            {
                throw new Exception("Invalid birth date format");
            }
            else if (!IsValidRole(admin.Role))
            {
                throw new Exception("Invalid role format");
            }
            else if (!IsValidStatus(admin.Status))
            {
                throw new Exception("Invalid status value");
            }
            ac.Update(admin);

        }


        public void Delete(Admin admin)
        {
            var ac = new AdminCrudFactory();
            ac.Delete(admin);
        }

        public void AssignRole(int adminId, int userId, string newRole, int branch)
        {
            
            var admin = RetrieveById(adminId);
            if (admin == null || admin.Role != "Admin")
            {
                throw new Exception("Only administrators can assign roles.");
            }

            if (!IsValidRole(newRole))
            {
                throw new Exception("Invalid role format");
            }

            // Obtener el usuario al que se le asignará el nuevo rol
            var um = new UserCrudFactory();
            var user = um.RetrieveById<User>(userId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }

           /* switch (user.Role)
            {
                case "Doctor":
                    var dc = new DoctorCrudFactory();
                    var doctor = new Doctor
                    {
                        Id= user.Id,
                    };
                    dc.Delete(doctor);
                    break;
                case "Nurse":
                    var nf = new NurseCrudFactory();
                    var nurse = new Nurse
                    {
                        Id = user.Id,
                    };
                    nf.Delete(nurse);
                    break;
                case "Secretary":
                    var sf = new SecretaryCrudFactory();
                    var secretary = new Secretary
                    {
                        Id = user.Id
                    };
                    sf.Delete(secretary);
                    break;

            }*/


            user.Role = newRole;
            um.Update(user);

            switch (newRole)
            {
                case "Patient":
                    var pm = new PatientCrudFactory();
                    var patient = new Patient
                    {
                        Name = user.Name,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        Password = user.Password,
                        Sex = user.Sex,
                        BirthDate = user.BirthDate,
                        Role = newRole,
                        Status = user.Status,
                    };
                    pm.Create(patient);
                    break;
                case "Doctor":
                    var dc = new DoctorCrudFactory();
                    var doctor = new Doctor
                    {
                        Name = user.Name,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        Password = user.Password,
                        Sex = user.Sex,
                        BirthDate = user.BirthDate,
                        Role = newRole,
                        Status = user.Status,
                        BranchID = branch
                    };
                    dc.Create(doctor);
                    break;
                case "User":
                    break;
                case "Nurse":
                    var nf = new NurseCrudFactory();
                    var nurse = new Nurse
                    {
                        Name = user.Name,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        Password = user.Password,
                        Sex = user.Sex,
                        BirthDate = user.BirthDate,
                        Role = newRole,
                        Status = user.Status,
                        BranchId = branch
                    };
                    nf.Create(nurse);
                    break;
                case "Secretary":
                    var sf = new SecretaryCrudFactory();
                    var secretary = new Secretary
                    {
                        Name = user.Name,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        Email = user.Email,
                        Password = user.Password,
                        Sex = user.Sex,
                        BirthDate = user.BirthDate,
                        Role = newRole,
                        Status = user.Status,
                        BranchID=branch
                    };
                    sf.Create(secretary);
                    break;

            }
        }

        public void AssignSchedule(int staffId, string schedule, string staffType)
        {
            switch (staffType.ToLower())
            {
                case "doctor":
                    var doctor = new Doctor { Id = staffId, Schedule = schedule };
                    var doctorCrud = new DoctorCrudFactory();
                    doctorCrud.AddSchedule(doctor);
                    break;

                case "nurse":
                    var nurse = new Nurse { Id = staffId, Schedule = schedule };
                    var nurseCrud = new NurseCrudFactory();
                    nurseCrud.AddSchedule(nurse);
                    break;

                case "secretary":
                    var secretary = new Secretary { Id = staffId, Schedule = schedule };
                    var secretaryCrud = new SecretaryCrudFactory();
                    secretaryCrud.AddSchedule(secretary);
                    break;

                default:
                    throw new ArgumentException("Invalid staff type.");
            }
        }



        /// VALIDACIONES
        private bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && char.IsUpper(name[0]) && name.All(c => char.IsLetter(c) && !char.IsWhiteSpace(c));
        }

        private bool IsValidLastName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && char.IsUpper(name[0]) && name.All(c => char.IsLetter(c) && !char.IsWhiteSpace(c));
        }

        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            if (password.Length < 8)
                return false;

            bool hasNumber = false;
            bool hasSpecialCharacter = false;

            foreach (char c in password)
            {
                if (char.IsDigit(c))
                {
                    hasNumber = true;
                }
                else if (!char.IsLetterOrDigit(c))
                {
                    hasSpecialCharacter = true;
                }
            }
            return hasNumber && hasSpecialCharacter;
        }

        private bool IsValidSex(string sex)
        {
            if (string.IsNullOrWhiteSpace(sex))
                return false;
            string sexLower = sex.ToLower();
            return sexLower == "m" || sexLower == "f" || sexLower == "masculino" || sexLower == "femenino";
        }

        private bool IsValidEmail(string email)
        {

            string emailRegexPattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailRegexPattern);
        }

        private bool IsValidStatus(string status)
        {
            return !string.IsNullOrWhiteSpace(status) && char.IsUpper(status[0]) && status.All(c => char.IsLetter(c) || char.IsWhiteSpace(c));
        }



        private bool IsValidPhoneNumber(int phoneNumber)
        {

            return phoneNumber.ToString().Length == 8 && !phoneNumber.ToString().Contains(" ");
        }


        private bool IsValidRole(string role)
        {
            return !string.IsNullOrWhiteSpace(role) && char.IsUpper(role[0]) && role.All(c => char.IsLetter(c) && !char.IsWhiteSpace(c));
        }

        private bool IsValidAddress(string adress)
        {
            return !string.IsNullOrWhiteSpace(adress);
        }


        private bool IsValidBirthDate(DateTime birthDate)
        {
            string expectedFormat = "yyyy-MM-dd";

            return DateTime.TryParseExact(birthDate.ToString(expectedFormat), expectedFormat, null, DateTimeStyles.None, out _);
        }
    }
}

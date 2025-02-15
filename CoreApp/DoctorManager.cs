using DataAccess.CRUD;
using DataAccess.DAOs;
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
    public class DoctorManager
    {
        public void Create(Doctor doctor)
        {
            var dc = new DoctorCrudFactory();


            if (!IsValidName(doctor.Name))
            {
                throw new Exception("Invalid name format");
            }
            else if (!IsValidLastName(doctor.LastName))
            {
                throw new Exception("Invalid Lastname format");
            }
            else if (!IsValidPhoneNumber(doctor.PhoneNumber))
            {
                throw new Exception("Invalid phone number format");
            }
            else if (!IsValidEmail(doctor.Email))
            {
                throw new Exception("Email is required");
            }
            else if (!IsValidPassword(doctor.Password))
            {
                throw new Exception("Invalid Password format");
            }
            else if (!IsValidSex(doctor.Sex))
            {
                throw new Exception("Invalid Sex format");
            }
            else if (!IsValidBirthDate(doctor.BirthDate))
            {
                throw new Exception("Invalid birth date format");
            }
            else if (!IsValidRole(doctor.Role))
            {
                throw new Exception("Invalid role format");
            }
            else if (!IsValidStatus(doctor.Status))
            {
                throw new Exception("Invalid status value");
            }
            dc.Create(doctor);
        }

        public void AddSpecialty(Doctor doctor)
        {
            var dc = new DoctorCrudFactory();
            dc.AddSpecialty(doctor);
        }

        public void AddSchedule(Doctor doctor)
        {
            var dc = new DoctorCrudFactory();
            dc.AddSchedule(doctor);
        }

        public List<T> RetrieveAll<T>()
        {
            var dc = new DoctorCrudFactory();
            return dc.RetrieveAll<T>();
        }

        public List<T> RetrieveDoctorSpecialty<T>()
        {
            var dc = new DoctorCrudFactory();
            return dc.RetrieveDoctorSpecialty<T>();
        }

        public void UpdateDoctorSpecialty(DoctorSpecialty doctorSpecialty)
        {
            var dc = new DoctorCrudFactory();
            dc.UpdateDoctorSpecialty(doctorSpecialty);
        }

        public Doctor RetrieveById(int doctorID)
        {
            var dc = new DoctorCrudFactory();
            return dc.RetrieveById<Doctor>(doctorID);
        }

        public void Update(Doctor doctor)
        {
            var dc = new DoctorCrudFactory();

            if (!IsValidName(doctor.Name))
            {
                throw new Exception("Invalid name format");
            }
            else if (!IsValidLastName(doctor.LastName))
            {
                throw new Exception("Invalid Lastname format");
            }
            else if (!IsValidPhoneNumber(doctor.PhoneNumber))
            {
                throw new Exception("Invalid phone number format");
            }
            else if (!IsValidEmail(doctor.Email))
            {
                throw new Exception("Email is required");
            }
            else if (!IsValidPassword(doctor.Password))
            {
                throw new Exception("Invalid Password format");
            }
            else if (!IsValidSex(doctor.Sex))
            {
                throw new Exception("Invalid Sex format");
            }
            else if (!IsValidBirthDate(doctor.BirthDate))
            {
                throw new Exception("Invalid birth date format");
            }
            else if (!IsValidRole(doctor.Role))
            {
                throw new Exception("Invalid role format");
            }
            else if (!IsValidStatus(doctor.Status))
            {
                throw new Exception("Invalid status value");
            }
            else if (!IsValidID(doctor.BranchID))
            {
                throw new Exception("Invalid Branch ID");
            }
            dc.Update(doctor);

        }

        public void Delete(Doctor doctor)
        {
            var dc = new DoctorCrudFactory();
            dc.Delete(doctor);
        }

        public List<string> GetSpecialtiesByBranch(int branchId)
        {
            var dc = new DoctorCrudFactory();
            return dc.GetSpecialtiesByBranch(branchId);
        }

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
            // Verifica si la contraseña no es nula o compuesta solamente de espacios en blanco
            if (string.IsNullOrWhiteSpace(password))
                return false;

            // Verifica si la contraseña tiene al menos 8 caracteres
            if (password.Length < 8)
                return false;

            bool hasNumber = false;
            bool hasSpecialCharacter = false;

            foreach (char c in password)
            {
                // Verifica si el carácter actual es un número
                if (char.IsDigit(c))
                {
                    hasNumber = true;
                }
                // Verifica si el carácter actual es un carácter especial
                else if (!char.IsLetterOrDigit(c))
                {
                    hasSpecialCharacter = true;
                }
            }

            // Retorna verdadero si la contraseña tiene al menos un número y un carácter especial
            return hasNumber && hasSpecialCharacter;
        }

        private bool IsValidSex(string sex)
        {
            // Verifica si el sexo no es nulo o compuesto solamente de espacios en blanco
            if (string.IsNullOrWhiteSpace(sex))
                return false;

            // Convierte el sexo a minúsculas para hacer la comparación más fácil
            string sexLower = sex.ToLower();

            // Verifica si el sexo es válido
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

        private bool IsValidAdress(string adress)
        {
            return !string.IsNullOrWhiteSpace(adress);
        }


        private bool IsValidBirthDate(DateTime birthDate)
        {
            string expectedFormat = "yyyy-MM-dd";

            return DateTime.TryParseExact(birthDate.ToString(expectedFormat), expectedFormat, null, DateTimeStyles.None, out _);
        }

        private bool IsValidID(int id)
        {
            return id > 0;
        }
    }
}


using CoreApp;
using DataAccess.CRUD;
using DTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Formatting = Newtonsoft.Json.Formatting;

namespace TestConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Program_menu();
            }

        }
        static void Program_menu()
        {
            Console.WriteLine("\nProgram Menu" +
           "\n1. User Info" +
           "\n2. Nurse Info" +
           "\n3. Patient Info" +
           "\n4. Exit" +
           "\n6. Appointment Info"
            );

            var opc = Console.ReadLine();

            switch (opc)
            {
                case "1":
                    UserMenu();
                    break;
                case "2":
                    NurseMenu();
                    break;
                case "3":
                    PatientMenu();
                    break;
                case "6":
                    AppointmentMenu();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
            }
        }

        //METODOS DE APPOINTMENT
        private static void AppointmentMenu()
        {
            // throw new NotImplementedException();
            while (true)
            {
                Console.WriteLine("\n Appointment Menu" +
                 "\n1. Create Appointment" +
                 "\n2. Delete Appointment" +
                 "\n3. Update Appointment" +
                 "\n4. Search Appointment by Id" +
                 "\n5. Get all appointments" +
                 "\n0. Return to main menu");
                var opc_user = Console.ReadLine();
                switch (opc_user)
                {
                    case "1":
                        CreateAppointment();
                        break;
                    case "2":
                        DeleteAppointment();
                        break;
                    case "3":
                        UpdateAppointment();
                        break;
                    case "4":
                        ShowAppointmentById();
                        break;
                    case "5":
                        ShowAllAppointments();
                        break;
                    case "0":
                        Program_menu();
                        break;
                 
                }
            }
        }

        private static void UpdateAppointment()
        {
            //throw new NotImplementedException();
            Console.WriteLine("__________________");
            Console.WriteLine("APPOINTMENT UPDATE PAGE");

            Console.WriteLine("Enter the id for the appointment you wish to UPDATE");
            var id = Console.ReadLine();
            Console.WriteLine("Enter Patient Id");
            var patientId = Console.ReadLine();

            Console.WriteLine("Enter Doctor Id");
            var doctorId = Console.ReadLine();


            Console.WriteLine("Enter Service Id");
            var serviceId = Console.ReadLine();


            Console.WriteLine("Enter Branch Id");
            var branchId = Console.ReadLine();


            Console.WriteLine("Enter Appointment start time");
            var startTime = Console.ReadLine();

            Console.WriteLine("Enter Appointment start time");
            var endTime = Console.ReadLine();

            Console.WriteLine("Enter Appointment motive");
            var motive = Console.ReadLine();


            var status = "Payment Pending";

            var newAppointment = new Appointment()
            {
                PatientId = int.Parse(patientId),
                DoctorId = int.Parse(doctorId),
                ServiceId = int.Parse(serviceId),
                BranchId = int.Parse(branchId),
                StartTime = DateTime.Parse(startTime),
                EndTime = DateTime.Parse(endTime),
                Text = motive,
                Status = status
            };

            try
            {
                // Vamos a agarrar el usuario en db
                var apc = new AppointmentCrudFactory();
                apc.Update(newAppointment);
                Console.WriteLine("Yohoo! Appointment updated");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();
        }

        private static void ShowAllAppointments()
        {
            //throw new NotImplementedException();
            Console.WriteLine("____________________");
            Console.WriteLine("All Appointments List");

            try
            {
                var appointmentCrudFactory = new AppointmentCrudFactory();
                var appointments = appointmentCrudFactory.RetrieveAll<Appointment>();

                if (appointments.Count > 0)
                {
                    Console.WriteLine("\nAll Appointments:\n");

                    foreach (var appointment in appointments)
                    {
                        Console.WriteLine($"Appointment ID: {appointment.Id}");
                        Console.WriteLine($"Patient ID: {appointment.PatientId}");
                        Console.WriteLine($"Patient name: {appointment.PatientName}");
                        Console.WriteLine($"Patient last name: {appointment.PatientLastName}");
                        Console.WriteLine($"Doctor ID: {appointment.DoctorId}");
                        Console.WriteLine($"Doctor name: {appointment.DoctorName}");
                        Console.WriteLine($"Doctor last name: {appointment.DoctorLastName}");
                        Console.WriteLine($"Service ID: {appointment.ServiceId}");
                        Console.WriteLine($"Service name: {appointment.ServiceName}");
                        Console.WriteLine($"Branch ID: {appointment.BranchId}");
                        Console.WriteLine($"Branch Name: {appointment.BranchName}");
                        Console.WriteLine($"Start time: {appointment.StartTime}");
                        Console.WriteLine($"End time: {appointment.EndTime}");
                        Console.WriteLine($"Motive: {appointment.Text}");
                        Console.WriteLine($"Status: {appointment.Status}");
                        Console.WriteLine("-------------------------------------------");
                    }
                }
                else
                {
                    Console.WriteLine("No appointments found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        private static void ShowAppointmentById()
        {
            //throw new NotImplementedException();
            Console.WriteLine("____________________");
            Console.WriteLine("APPOINTMENT DETAILS PAGE");

            Console.WriteLine("Enter the appointment Id you wish to see:");
            if (int.TryParse(Console.ReadLine(), out int apptId))
            {
                try
                {
                    var appointmentCrudFactory = new AppointmentCrudFactory();
                    var appointment = appointmentCrudFactory.RetrieveById<Appointment>(apptId);

                    if (appointment != null)
                    {
                        Console.WriteLine($"Appointment ID: {appointment.Id}");
                        Console.WriteLine($"Patient ID: {appointment.PatientId}");
                        Console.WriteLine($"Patient name: {appointment.PatientName}");
                        Console.WriteLine($"Patient last name: {appointment.PatientLastName}");
                        Console.WriteLine($"Doctor ID: {appointment.DoctorId}");
                        Console.WriteLine($"Doctor name: {appointment.DoctorName}");
                        Console.WriteLine($"Doctor last name: {appointment.DoctorLastName}");
                        Console.WriteLine($"Service ID: {appointment.ServiceId}");
                        Console.WriteLine($"Service name: {appointment.ServiceName}");
                        Console.WriteLine($"Branch ID: {appointment.BranchId}");
                        Console.WriteLine($"Branch Name: {appointment.BranchName}");
                        Console.WriteLine($"Start time: {appointment.StartTime}");
                        Console.WriteLine($"End time: {appointment.EndTime}");
                        Console.WriteLine($"Motive: {appointment.Text}");
                        Console.WriteLine($"Status: {appointment.Status}");
                        Console.WriteLine("-------------------------------------------");

                    }
                    else
                    {
                        Console.WriteLine($"Appointment with ID {apptId} not found.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid ID.");
            }
        }

        private static void DeleteAppointment()
        {
            //throw new NotImplementedException();

            Console.WriteLine("_________________________");
            Console.WriteLine("Appointment Delete page");

            Console.WriteLine("Enter the Id for the appointment you wish to delete");
            var ApptId = Console.ReadLine();

            //crear nuevo objeto Appointment
            var newAppt = new Appointment()
            {
                Id = int.Parse(ApptId)
            };

            try
            {
                // Vamos a borrar el Asset en db
                var apc = new AppointmentCrudFactory();
                apc.Delete(newAppt);
                Console.WriteLine("Appointment deleted");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();

        }

        private static void CreateAppointment()
        {
            // throw new NotImplementedException();

            Console.WriteLine("_________________________");
            Console.WriteLine("Appointment creation page");

            Console.WriteLine("Enter Patient Id");
            var patientId = Console.ReadLine();

            Console.WriteLine("Enter Doctor Id");
            var doctorId = Console.ReadLine();


            Console.WriteLine("Enter Service Id");
            var serviceId = Console.ReadLine();


            Console.WriteLine("Enter Branch Id");
            var branchId = Console.ReadLine();


            Console.WriteLine("Enter Appointment start time");
            var startTime = Console.ReadLine();

            Console.WriteLine("Enter Appointment start time");
            var endTime = Console.ReadLine();

            Console.WriteLine("Enter Appointment motive");
            var motive = Console.ReadLine();


            var status = "Payment Pending";

            //crear nuevo objeto Appointment
            var newAppt = new Appointment()
            {
                PatientId = int.Parse(patientId),
                DoctorId = int.Parse(doctorId),
                ServiceId = int.Parse(serviceId),
                BranchId = int.Parse(branchId),
                StartTime = DateTime.Parse(startTime),
                EndTime = DateTime.Parse(endTime),
                Text = motive,
                Status = status
            };


            //Creacion del user 
            try
            {
                var apc = new AppointmentCrudFactory();
                apc.Create(newAppt);
                Console.WriteLine("Appointment made");
            }catch(Exception ex) 
            {
                Console.WriteLine( ex.ToString);
            }
            Console.ReadLine();
        }

        // USER MENU 

        static void UserMenu()
        {
            while (true)
            {
                Console.WriteLine("\nUser Menu" +
                 "\n1. Create User" +
                 "\n2. Delete User" +
                 "\n3. Update User" +
                 "\n4. List Users" +
                 "\n5. Search user by Id" +
                 "\n6. Return to main menu" +
                 "\n7. Exit program");
                var opc_user = Console.ReadLine();
                switch (opc_user)
                {
                    case "1":
                        CreateUser();
                        break;
                    case "2":
                        DeleteUser();
                        break;
                    case "3":
                        UpdateUser();
                        break;
                    case "4":
                        ListUser();
                        break;
                    case "5":
                        SearchUserById();
                        break;
                    case "6":
                        Program_menu();
                        break;
                    case "7":
                        Environment.Exit(0);
                        break;
                }
            }

        }

		

		static void CreateUser()
        {
            Console.WriteLine("\nCreate user test");

            Console.WriteLine("\nEnter user name");
            var name = Console.ReadLine();

            Console.WriteLine("\nEnter user last name");
            var lastname = Console.ReadLine();

            Console.WriteLine("\nEnter user phone number");
            var phonenumber = Console.ReadLine();

            Console.WriteLine("\nEnter user email");
            var email = Console.ReadLine();

            Console.WriteLine("\nEnter user password");
            var password = Console.ReadLine();

            Console.WriteLine("\nEnter user sex");
            var sex = Console.ReadLine();

            Console.WriteLine("\nEnter user birthdate (yyyy-MM-dd)");
            var birthdateString = Console.ReadLine();

            DateTime birthdate;
            if (!DateTime.TryParse(birthdateString, out birthdate))
            {
                Console.WriteLine("Invalid date format. Please enter date in yyyy-MM-dd format.");
                return;
            }

            Console.WriteLine("\nEnter user role");
            var role = Console.ReadLine();

            Console.WriteLine("\nEnter user status");
            var status = Console.ReadLine();

            Console.WriteLine("\nEnter user adress");
            var adress = Console.ReadLine();

            var newUser = new User()
            {
                Name = name,
                LastName = lastname,
                PhoneNumber = int.Parse(phonenumber),
                Email = email,
                Password = password,
                Sex = sex,
                BirthDate = birthdate,
                Role = role,
                Status = status,
                
            };

            try
            {
                var um = new UserManager();
                um.Create(newUser);
                Console.WriteLine("\nUser Created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }



            Console.ReadLine();
        }

        static void DeleteUser()
        {
            Console.WriteLine("\n Delete User ");

            Console.WriteLine("\nEnter User's id to delete:");
            string userInput = Console.ReadLine();

            try
            {
                int userIdToDelete = int.Parse(userInput);

                var userToDelete = new User { Id = userIdToDelete };

                var um = new UserManager();
                um.Delete(userToDelete);
                Console.WriteLine("\nUser Deleted");
            }
            catch (FormatException)
            {
                Console.WriteLine("\nInvalid user ID. Please enter a valid integer.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError Deleting user: {ex.Message}");
            }
        }

        static void UpdateUser()

        {
            Console.WriteLine("\nUpdate User");

            Console.WriteLine("\nEnter User's id to update:");
            string userInput = Console.ReadLine();

            try
            {
                int userIdToUpdate = int.Parse(userInput);

                var userToUpdate = new User { Id = userIdToUpdate };
                var um = new UserManager();



                if (userToUpdate != null)
                {

                    Console.WriteLine("\nEnter new user information:");

                    Console.WriteLine("\nEnter user name");
                    userToUpdate.Name = Console.ReadLine();

                    Console.WriteLine("\nEnter user last name");
                    userToUpdate.LastName = Console.ReadLine();

                    Console.WriteLine("\nEnter user phone number");
                    userToUpdate.PhoneNumber = int.Parse(Console.ReadLine());

                    Console.WriteLine("\nEnter user email");
                    userToUpdate.Email = Console.ReadLine();

                    Console.WriteLine("\nEnter user password");
                    userToUpdate.Password = Console.ReadLine();

                    Console.WriteLine("\nEnter user sex");
                    userToUpdate.Sex = Console.ReadLine();

                    Console.WriteLine("\nEnter user birthdate (yyyy-MM-dd)");
                    var birthdateString = Console.ReadLine();

                    DateTime birthdate;
                    if (!DateTime.TryParse(birthdateString, out birthdate))
                    {
                        Console.WriteLine("Invalid date format. Please enter date in yyyy-MM-dd format.");
                        return;
                    }
                    userToUpdate.BirthDate = birthdate;

                    Console.WriteLine("\nEnter user role");
                    userToUpdate.Role = Console.ReadLine();

                    Console.WriteLine("\nEnter user status");
                    userToUpdate.Status = Console.ReadLine();

                    Console.WriteLine("\nEnter user adress");

                    Console.WriteLine("\nUser Updated");

                    um.Update(userToUpdate);
                }
                else
                {
                    Console.WriteLine("\nUser not found with the given ID.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nInvalid user ID. Please enter a valid integer.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError updating user: {ex.Message}");
            }
        }

        static void ListUser()
        {
            var um = new UserManager();
            List<User> userList = um.RetrieveAll();
            if (userList.Count > 0)
            {
                // Serializar la lista de usuarios a formato JSON
                string jsonUsers = JsonConvert.SerializeObject(userList, Formatting.Indented);

                // Imprimir el JSON resultante
                Console.WriteLine("\tLista de Usuarios");
                Console.WriteLine(jsonUsers);
            }
            else
            {
                Console.WriteLine("No se encontraron usuarios.");
            }
        }

        static void SearchUserById()
        {

            Console.Write("Ingrese el ID del usuario a buscar: ");
            if (int.TryParse(Console.ReadLine(), out int userId))
            {
                var um = new UserManager();
                User user = um.RetrieveById(userId);

                if (user != null)
                {

                    string jsonUser = JsonConvert.SerializeObject(user, Formatting.Indented);
                    Console.WriteLine("\nDatos del Usuario con el Id numero: " + userId);
                    Console.WriteLine(jsonUser);
                }
                else
                {
                    Console.WriteLine($"No se encontró ningún usuario con el ID {userId}");
                }
            }
            else
            {
                Console.WriteLine("Entrada no válida para el ID del usuario.");
            }
        }

        static void NurseMenu()
        {
            while (true)
            {
                Console.WriteLine("\nUser Menu" +
                 "\n1. Create Nurse" +
                 "\n2. Delete Nurse" +
                 "\n3. Update Nurse" +
                 "\n4. List Nurse" +
                 "\n5. Search Nurse by Id" +
                 "\n6. Return to main menu" +
                 "\n7. Exit program");
                var opc_nurse = Console.ReadLine();
                switch (opc_nurse)
                {
                    case "1":
                        CreateNurse();
                        break;
                    case "2":
                        DeleteNurse();
                        break;
                    case "3":
                        UpdateNurse();
                        break;
                    case "4":
                        ListNurse();
                        break;
                    case "5":
                        SearchNurseById();
                        break;
                    case "6":
                        Program_menu();
                        break;
                    case "7":
                        Environment.Exit(0);
                        break;
                }
            }

        }

        static void CreateNurse()
        {
			Console.WriteLine("\nCreate nurse test");

			Console.WriteLine("\nEnter nurse name");
			var name = Console.ReadLine();

            Console.WriteLine("\nEnter nurse Branch");
            var branch = Console.ReadLine();

			Console.WriteLine("\nEnter nurse last name");
			var lastname = Console.ReadLine();

			Console.WriteLine("\nEnter nurse phone number");
			var phonenumber = Console.ReadLine();

			Console.WriteLine("\nEnter nurse email");
			var email = Console.ReadLine();

			Console.WriteLine("\nEnter nurse password");
			var password = Console.ReadLine();

			Console.WriteLine("\nEnter nurse sex");
			var sex = Console.ReadLine();

			Console.WriteLine("\nEnter nurse birthdate (yyyy-MM-dd)");
			var birthdateString = Console.ReadLine();

			DateTime birthdate;
			if (!DateTime.TryParse(birthdateString, out birthdate))
			{
				Console.WriteLine("Invalid date format. Please enter date in yyyy-MM-dd format.");
				return;
			}

			Console.WriteLine("\nEnter nurse role");
			var role = Console.ReadLine();

			Console.WriteLine("\nEnter nurse status");
			var status = Console.ReadLine();

			Console.WriteLine("\nEnter nurse adress");
			var address = Console.ReadLine();

			var newNurse = new Nurse()
			{
				Name = name,
				BranchId = int.Parse(branch),
				LastName = lastname,
				PhoneNumber = int.Parse(phonenumber),
				Email = email,
				Password = password,
				Sex = sex,
				BirthDate = birthdate,
				Role = role,
				Status = status,



			};

			try
			{
				var um = new NurseManager();
				um.Create(newNurse);
				Console.WriteLine("\nNurse Created");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}



			Console.ReadLine();
		}

        static void DeleteNurse() 
        {
			Console.WriteLine("\n Delete Nurse ");

			Console.WriteLine("\nEnter Nurse's id to delete:");
			string nurseInput = Console.ReadLine();

			try
			{
				int nurseIdToDelete = int.Parse(nurseInput);

				var nurseToDelete = new Nurse { Id = nurseIdToDelete };

				var um = new NurseManager();
				um.Delete(nurseToDelete);
				Console.WriteLine("\nNurse Deleted");
			}
			catch (FormatException)
			{
				Console.WriteLine("\nInvalid nurse ID. Please enter a valid integer.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"\nError Deleting nurse: {ex.Message}");
			}
		}

        static void UpdateNurse()
        {
			Console.WriteLine("\nUpdate Nurse");

			Console.WriteLine("\nEnter Nurse's id to update:");
			string nurseInput = Console.ReadLine();

			try
			{
				int nurseIdToUpdate = int.Parse(nurseInput);

				var nurseToUpdate = new Nurse { Id = nurseIdToUpdate };
				var nm = new NurseManager();

				if (nurseToUpdate != null)
				{
					Console.WriteLine("\nEnter new nurse information:");

                    Console.WriteLine("\nEnter new nurse branch");
                    nurseToUpdate.BranchId = int.Parse(Console.ReadLine());

					Console.WriteLine("\nEnter nurse name");
					nurseToUpdate.Name = Console.ReadLine();

					Console.WriteLine("\nEnter nurse last name");
					nurseToUpdate.LastName = Console.ReadLine();

					Console.WriteLine("\nEnter nurse phone number");
					nurseToUpdate.PhoneNumber = int.Parse(Console.ReadLine());

					Console.WriteLine("\nEnter nurse email");
					nurseToUpdate.Email = Console.ReadLine();

					Console.WriteLine("\nEnter nurse password");
					nurseToUpdate.Password = Console.ReadLine();

					Console.WriteLine("\nEnter nurse sex");
					nurseToUpdate.Sex = Console.ReadLine();

					Console.WriteLine("\nEnter nurse birthdate (yyyy-MM-dd)");
					var birthdateString = Console.ReadLine();

					DateTime birthdate;
					if (!DateTime.TryParse(birthdateString, out birthdate))
					{
						Console.WriteLine("Invalid date format. Please enter date in yyyy-MM-dd format.");
						return;
					}
					nurseToUpdate.BirthDate = birthdate;

					Console.WriteLine("\nEnter nurse role");
					nurseToUpdate.Role = Console.ReadLine();

					Console.WriteLine("\nEnter nurse status");
					nurseToUpdate.Status = Console.ReadLine();

					Console.WriteLine("\nEnter nurse address");

					Console.WriteLine("\nNurse Updated");

					nm.Update(nurseToUpdate);
				}
				else
				{
					Console.WriteLine("\nNurse not found with the given ID.");
				}
			}
			catch (FormatException)
			{
				Console.WriteLine("\nInvalid nurse ID. Please enter a valid integer.");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"\nError updating nurse: {ex.Message}");
			}
		}

        static void SearchNurseById()
        {
			Console.Write("Ingrese el ID del usuario a buscar: ");
			if (int.TryParse(Console.ReadLine(), out int nurseId))
			{
				var um = new NurseManager();
				Nurse nurse = um.RetrieveById(nurseId);

				if (nurse != null)
				{

					string jsonNurse = JsonConvert.SerializeObject(nurse, Formatting.Indented);
					Console.WriteLine("\nDatos del Usuario con el Id numero: " + nurseId);
					Console.WriteLine(jsonNurse);
				}
				else
				{
					Console.WriteLine($"No se encontró ningún usuario con el ID {nurseId}");
				}
			}
			else
			{
				Console.WriteLine("Entrada no válida para el ID del usuario.");
			}
		}
		static void ListNurse()
		{
			var um = new NurseManager();
			List<Nurse> nurseList = um.RetrieveAll();
			if (nurseList.Count > 0)
			{
				// Serializar la lista de usuarios a formato JSON
				string jsonNurse = JsonConvert.SerializeObject(nurseList, Formatting.Indented);

				// Imprimir el JSON resultante
				Console.WriteLine("\tLista de Usuarios");
				Console.WriteLine(jsonNurse);
			}
			else
			{
				Console.WriteLine("No se encontraron usuarios.");
			}
		}

        static void PatientMenu()
        {
            while (true)
            {
                Console.WriteLine("\nPatient Menu" +
                 "\n1. Create Patient" +
                 "\n2. Delete Patient" +
                 "\n3. Update Patient" +
                 "\n4. List Patient" +
                 "\n5. Search Patient by Id" +
                 "\n6. Return to main menu" +
                 "\n7. Exit program");
                var opc_user = Console.ReadLine();
                switch (opc_user)
                {
                    case "1":
                        CreatePatient();
                        break;
                    case "2":
                        DeletePatient();
                        break;
                    case "3":
                        UpdatePatient();
                        break;
                    case "4":
                        ListPatient();
                        break;
                    case "5":
                        SearchPatientById();
                        break;
                    case "6":
                        Program_menu();
                        break;
                    case "7":
                        Environment.Exit(0);
                        break;
                }
            }

        }

        static void CreatePatient()
        {
            Console.WriteLine("\nCreate Patient ");

            Console.WriteLine("\nEnter Patient name");
            var name = Console.ReadLine();

            Console.WriteLine("\nEnter Patient last name");
            var lastname = Console.ReadLine();

            Console.WriteLine("\nEnter Patient phone number");
            var phonenumber = Console.ReadLine();

            Console.WriteLine("\nEnter Patient email");
            var email = Console.ReadLine();

            Console.WriteLine("\nEnter Patient password");
            var password = Console.ReadLine();

            Console.WriteLine("\nEnter Patient sex");
            var sex = Console.ReadLine();

            Console.WriteLine("\nEnter Patient birthdate (yyyy-MM-dd)");
            var birthdateString = Console.ReadLine();

            DateTime birthdate;
            if (!DateTime.TryParse(birthdateString, out birthdate))
            {
                Console.WriteLine("Invalid date format. Please enter date in yyyy-MM-dd format.");
                return;
            }

            var role = "Paciente";


            var status = "Moroso";

            Console.WriteLine("\nEnter user adress");
            var adress = Console.ReadLine();

            var newPatient = new Patient()
            {
                Name = name,
                LastName = lastname,
                PhoneNumber = int.Parse(phonenumber),
                Email = email,
                Password = password,
                Sex = sex,
                BirthDate = birthdate,
                Role = role,
                Status = status,



            };

            try
            {
                var pm = new PatientManager();
                pm.Create(newPatient);
                Console.WriteLine("\nPatient Created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }



            Console.ReadLine();
        }

        static void DeletePatient()
        {
            Console.WriteLine("\n Patient to Delete ");

            Console.WriteLine("\nEnter Patient id to delete:");
            string userInput = Console.ReadLine();

            try
            {
                int patientIdToDelete = int.Parse(userInput);

                var patientToDelete = new Patient { Id = patientIdToDelete };

                var pm = new PatientManager();
                pm.Delete(patientToDelete);
                Console.WriteLine("\nPatient Deleted");
            }
            catch (FormatException)
            {
                Console.WriteLine("\nInvalid patient ID. Please enter a valid integer.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError Deleting patient: {ex.Message}");
            }
        }

        static void UpdatePatient()

        {
            Console.WriteLine("\nUpdate Patient");

            Console.WriteLine("\nEnter Patient id to update:");
            string userInput = Console.ReadLine();

            try
            {
                int patientIdToUpdate = int.Parse(userInput);

                var patientToUpdate = new Patient { Id = patientIdToUpdate };
                var pm = new PatientManager();



                if (patientToUpdate != null)
                {

                    Console.WriteLine("\nEnter new patient information:");

                    Console.WriteLine("\nEnter patient name");
                    patientToUpdate.Name = Console.ReadLine();

                    Console.WriteLine("\nEnter patient last name");
                    patientToUpdate.LastName = Console.ReadLine();

                    Console.WriteLine("\nEnter patient phone number");
                    patientToUpdate.PhoneNumber = int.Parse(Console.ReadLine());

                    Console.WriteLine("\nEnter patient email");
                    patientToUpdate.Email = Console.ReadLine();

                    Console.WriteLine("\nEnter patient password");
                    patientToUpdate.Password = Console.ReadLine();

                    Console.WriteLine("\nEnter patient sex");
                    patientToUpdate.Sex = Console.ReadLine();

                    Console.WriteLine("\nEnter patient birthdate (yyyy-MM-dd)");
                    var birthdateString = Console.ReadLine();

                    DateTime birthdate;
                    if (!DateTime.TryParse(birthdateString, out birthdate))
                    {
                        Console.WriteLine("Invalid date format. Please enter date in yyyy-MM-dd format.");
                        return;
                    }
                    patientToUpdate.BirthDate = birthdate;

                    Console.WriteLine("\nEnter patient role");
                    patientToUpdate.Role = Console.ReadLine();

                    Console.WriteLine("\nEnter patient status");
                    patientToUpdate.Status = Console.ReadLine();

                    Console.WriteLine("\nEnter patient adress");

                    Console.WriteLine("\n{Patient Updated");

                    pm.Update(patientToUpdate);
                }
                else
                {
                    Console.WriteLine("\nPatient not found with the given ID.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nInvalid patient ID. Please enter a valid integer.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError updating patient: {ex.Message}");
            }
        }

        static void ListPatient()
        {
            var pm = new PatientManager();
            List<Patient> patientList = pm.RetrieveAll();
            if (patientList.Count > 0)
            {
                // Serializar la lista de usuarios a formato JSON
                string jsonPatients = JsonConvert.SerializeObject(patientList, Formatting.Indented);

                // Imprimir el JSON resultante
                Console.WriteLine("\tLista de Pacientes");
                Console.WriteLine(jsonPatients);
            }
            else
            {
                Console.WriteLine("No se encontraron pacientes.");
            }
        }

        static void SearchPatientById()
        {

            Console.Write("Ingrese el ID del paciente a buscar: ");
            if (int.TryParse(Console.ReadLine(), out int patientId))
            {
                var pm = new PatientManager();
                Patient patient = pm.RetrieveById(patientId);

                if (patient != null)
                {

                    string jsonPatient = JsonConvert.SerializeObject(patient, Formatting.Indented);
                    Console.WriteLine("\nDatos del Paciente con el Id numero: " + patientId);
                    Console.WriteLine(jsonPatient);
                }
                else
                {
                    Console.WriteLine($"No se encontró ningún paciente con el ID {patientId}");
                }
            }
            else
            {
                Console.WriteLine("Entrada no válida para el ID del paciente.");
            }
        }

        static void BranchMenu()
        {
            while (true)
            {
                Console.WriteLine("\nBranch Menu" +
                 "\n1. Create Branch" +
                 "\n2. Delete Branch" +
                 "\n3. Update Branch" +
                 "\n4. List Branch" +
                 "\n5. Search Branch by Id" +
                 "\n6. Return to main menu" +
                 "\n7. Exit program");
                var opc_branch = Console.ReadLine();
                switch (opc_branch)
                {
                    case "1":
                        CreateBranch();
                        break;
                    case "2":
                        DeleteBranch();
                        break;
                    case "3":
                        UpdateBranch();
                        break;
                    case "4":
                        ListBranch();
                        break;
                    case "5":
                        SearchBranchById();
                        break;
                    case "6":
                        Program_menu();
                        break;
                    case "7":
                        Environment.Exit(0);
                        break;
                }
            }

        }

        static void CreateBranch()
        {
            Console.WriteLine("\nCreate Branch test");

            Console.WriteLine("\nEnter Branch name");
            var name = Console.ReadLine();

            Console.WriteLine("\nEnter Branch Address");
            var address = Console.ReadLine();

            Console.WriteLine("\nEnter Branch Description");
            var description = Console.ReadLine();

            Console.WriteLine("\nEnter Branch Creation Date (yyyy-MM-dd)");
            var creationDate = Console.ReadLine();


            /*
            
            
            DateTime creationDate;
            if (!DateTime.TryParse(creationDateString, out creationDate))
            {
                Console.WriteLine("Invalid date format. Please enter date in yyyy-MM-dd format.");
                return;
            }
            */
            var newBranch = new Branch()
            {
                Name = name,
                Address = address,
                Description = description,
            };

            try
            {
                var um = new BranchCrudFactory();
                um.Create(newBranch);
                Console.WriteLine("\nBranch Created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            

            Console.ReadLine();
        }

        static void DeleteBranch()
        {
            Console.WriteLine("\n Delete Branch ");

            Console.WriteLine("\nEnter Branch's id to delete:");
            string branchInput = Console.ReadLine();

            
            try
            {
                int branchIdToDelete = int.Parse(branchInput);

                var branchToDelete = new Branch { Id = branchIdToDelete };

                var um = new BranchCrudFactory();
                um.Delete(branchToDelete);
                Console.WriteLine("\nBranch Deleted");
            }
            catch (FormatException)
            {
                Console.WriteLine("\nInvalid branch ID. Please enter a valid integer.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError Deleting branch: {ex.Message}");
            }
        }

        static void UpdateBranch()

        {
            Console.WriteLine("\nUpdate Branch");

            Console.WriteLine("\nEnter Branch's id to update:");
            string branchInput = Console.ReadLine();

            try
            {
                int branchIdToUpdate = int.Parse(branchInput);

                var branchToUpdate = new Branch { Id = branchIdToUpdate };
                var um = new BranchCrudFactory();



                if (branchToUpdate != null)
                {

                    Console.WriteLine("\nEnter new branch information:");

                    Console.WriteLine("\nEnter user name");
                    branchToUpdate.Name = Console.ReadLine();

                    Console.WriteLine("\nEnter branch address");
                    branchToUpdate.Address = Console.ReadLine();

                    Console.WriteLine("\nEnter branch description");
                    branchToUpdate.Description = Console.ReadLine();

                    Console.WriteLine("\nEnter branch creation date (yyyy-MM-dd)");
                    var creationDate = Console.ReadLine();

                    /*
                    DateTime creationDate;
                    if (!DateTime.TryParse(creationDateString, out creationDate))
                    {
                        Console.WriteLine("Invalid date format. Please enter date in yyyy-MM-dd format.");
                        return;
                    } 
                    
                    branchIdToUpdate.CreationDate = creationDate;
                    */

                    Console.WriteLine("\nBranch Updated");

                    um.Update(branchToUpdate);
                }
                else
                {
                    Console.WriteLine("\nBranch not found with the given ID.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nInvalid branch ID. Please enter a valid integer.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError updating branch: {ex.Message}");
            }
        }

        static void ListBranch()
        {
            var um = new BranchManager();
            List<Branch> branchList = um.RetrieveAll();
            if (branchList.Count > 0)
            {
                // Serializar la lista de sedes a formato JSON
                string jsonBranches = JsonConvert.SerializeObject(branchList, Formatting.Indented);

                // Imprimir el JSON resultante
                Console.WriteLine("\tLista de Sedes");
                Console.WriteLine(jsonBranches);
            }
            else
            {
                Console.WriteLine("No se encontraron sedes.");
            }
        }

        static void SearchBranchById()
        {

            Console.Write("Ingrese el ID del branch a buscar: ");
            if (int.TryParse(Console.ReadLine(), out int branchId))
            {
                var um = new BranchManager();
                Branch branch = um.RetrieveById(branchId);

                if (branch != null)
                {

                    string jsonBranch = JsonConvert.SerializeObject(branch, Formatting.Indented);
                    Console.WriteLine("\nDatos del branch con el Id numero: " + branchId);
                    Console.WriteLine(jsonBranch);
                }
                else
                {
                    Console.WriteLine($"No se encontró ningún branch con el ID {branchId}");
                }
            }
            else
            {
                Console.WriteLine("Entrada no válida para el ID del branch.");
            }
        }
    }
}

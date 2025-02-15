using CoreApp;
using DTO;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("RetrieveAll")]
        public ActionResult RetrieveAll()
        {
            try
            {
                var um = new UserManager();
                var userList = um.RetrieveAll();
                return Ok(userList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        [Route("RetrieveAllRoleUser")]
        public ActionResult RetrieveAllRoleUser()
        {
            try
            {
                var um = new UserManager();
                var userList = um.RetrieveAllRoleUser();
                return Ok(userList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        [Route("RetrieveById")]
        public ActionResult RetrieveById(int userId)
        {
            try
            {
                var um = new UserManager();
                var user = um.RetrieveById(userId);

                if (user != null)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("RetrieveRoleByUserEmail")]
        public ActionResult RetrieveRoleByUserEmail(UserForgotPasswordModel usr)
        {
            try
            {
                var um = new UserManager();
                var user = um.RetrieveRoleByUserEmail(usr.Email);

                if (user != null)
                {
                    return Ok(new {User = user, Role = user.Role});
                }
                else
                {
                    return NotFound("User not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        [Route("RetrieveUserByEmail")]
        public ActionResult RetrieveUserByEmail(string email)
        {
            try
            {
                var um = new UserManager();
                var userList = um.RetrieveUserByEmail(email);
                return Ok(userList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult> CreateAsync(User user)
        {
            try
            {
                var um = new UserManager();
                um.Create(user);
                return Ok(new { message = "User created" + user });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(User user)
        {
            try
            {
                var um = new UserManager();
                um.Update(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        [Route("UpdateUserData")]
        public ActionResult UpdateUserData(UserDataList userDataList)
        {
            try
            {
                var um = new UserManager();
                um.UpdateUserData(userDataList);
                return Ok(userDataList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        [Route("UpdateEmployeeData")]
        public ActionResult UpdateEmployeeData(Employee employee)
        {
            try
            {
                var um = new UserManager();
                um.UpdateEmployeeData(employee);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPut]
        [Route("UpdateUserRole")]
        public ActionResult UpdateUserRole(User user)
        {
            try
            {
                var um = new UserManager();
                um.UpdateUserRole(user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        [Route("RetrieveUsersByRole")]
        public ActionResult RetrieveUsersByRole(string role)
        {
            try
            {
                var um = new UserManager();
                var users = um.RetrieveUsersByRole(role);

                if (users != null)
                {
                    return Ok(users);
                }
                else
                {
                    return NotFound("No users found for the specified role");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(User user)
        {
            try
            {
                var um = new UserManager();
                um.Delete(user);
                return Ok(user);


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteUserData")]
        public ActionResult DeleteUserData(UserUpdData userUpdData)
        {
            try
            {
                var um = new UserManager();
                um.DeleteUserData(userUpdData);
                return Ok(userUpdData);


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<ActionResult> ForgotPassword([FromBody] UserForgotPasswordModel model)
        {
            try
            {
                if (model == null || string.IsNullOrWhiteSpace(model.Email))
                {
                    return BadRequest("Invalid model");
                }

                var um = new UserManager();
                um.ForgotPassword(model.Email);

                return Ok(new { message = "Operation successful" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

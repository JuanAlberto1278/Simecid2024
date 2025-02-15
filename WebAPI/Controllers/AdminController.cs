using CoreApp;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        //Siempre vamos a retornar 2 respuestas
        //200 --> OK
        //500 --> Internal server Error
        //Los retrieve trabajan con el verbo get de http
        [HttpGet]
        [Route("RetrieveAll")]
        public ActionResult RetrieveAll()
        {
            try
            {
                var am = new AdminManager();
                var adminList = am.RetrieveAll();
                return Ok(adminList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        [Route("RetrieveById")]
        public ActionResult RetrieveById(int adminId)
        {
            try
            {
                var am = new AdminManager();
                var admin = am.RetrieveById(adminId);

                if (admin != null)
                {
                    return Ok(admin);
                }
                else
                {
                    return NotFound("Admin not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost]
        [Route("Create")]
        public ActionResult Create(Admin admin)
        {
            try
            {
                var am = new AdminManager();
                am.Create(admin);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }



        }

       

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Admin admin)
        {
            try
            {
                var am = new AdminManager();
                am.Update(admin);
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpPost]
        [Route("AssignRole")]
        public ActionResult AssignRole(int adminId, int userId, string newRole, int branch)
        {
            try
            {
                var am = new AdminManager();
                am.AssignRole(adminId, userId, newRole, branch);
                return Ok($"Role '{newRole}' assigned successfully to user with ID {userId}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while assigning role: {ex.Message}");
            }
        }
        [HttpPost]
        [Route("AssignSchedule")]
        public ActionResult AssignSchedule(int staffId, string schedule, string staffType)
        {
            try
            {
                var am = new AdminManager();
                am.AssignSchedule(staffId, schedule, staffType);
                return Ok($"Schedule assigned successfully to {staffType} with ID {staffId}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while assigning schedule: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetSpecialtiesByBranch")]
        public ActionResult GetSpecialtiesByBranch(int branchId)
        {
            try
            {
                var adminManager = new AdminManager();
                var specialties = adminManager.GetSpecialtiesByBranch(branchId);
                return Ok(specialties);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(Admin admin)
        {
            try
            {
                var am = new AdminManager();
                am.Delete(admin);
                return Ok(admin);


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

using DataAccess.CRUD;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
        [Route("api/[controller]")]
        [ApiController]

        public class MedicalReportController : ControllerBase
    {

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(MedicalReport medicalReport)
        {
            try
            {
                var me = new MedicalReportCrudFactory();
                me.Create(medicalReport);
                return Ok(medicalReport);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        [Route("RetrieveAll")]
        public ActionResult RetrieveAll()
        {
            try
            {
                var mr = new MedicalReportCrudFactory();
                var lastMedicalReport = mr.RetrieveAll<MedicalReport>();
                return Ok(lastMedicalReport);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(MedicalReport medicalReport)
        {
            try
            {
                var mr = new MedicalReportCrudFactory();
                mr.Update(medicalReport);
                return Ok("Medical report updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(MedicalReport medicalReport)
        {
            try
            {
                var mr = new MedicalReportCrudFactory();
                mr.Delete(medicalReport);
                return Ok("Medical report deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveByEmail")]
        public ActionResult RetrieveMedicalReportByEmail(string userEmail)
        {
            try
            {
                var aptc = new MedicalReportCrudFactory();
                var lastMedicalReport = aptc.RetrieveMedicalReportByEmail<MedicalReport>(userEmail);
                return Ok(lastMedicalReport);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



    }
}

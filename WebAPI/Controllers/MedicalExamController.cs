using DataAccess.CRUD;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /*ATRIBUTOS: [Route("api/[controller]")] especifica la ruta base de la API,
     * y [ApiController] indica que la clase es un controlador de API.*/

    public class MedicalExamController : ControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(MedicalExam medicalExam)
        {
            try
            {
                var me = new MedicalExamCrudFactory();
                me.Create(medicalExam);
                return Ok(medicalExam);
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
                var me = new MedicalExamCrudFactory();
                var lastMedicalExam = me.RetrieveAll<MedicalExam>();
                return Ok(lastMedicalExam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(MedicalExam medicalExam)
        {
            try
            {
                var me = new MedicalExamCrudFactory();
                me.Update(medicalExam);
                return Ok("Medical exam updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(MedicalExam medicalExam)
        {
            try
            {
                var me = new MedicalExamCrudFactory();
                me.Delete(medicalExam);
                return Ok("Medical exam deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveByEmail")]
        public ActionResult RetrieveMedicalExamByEmail(string userEmail)
        {
            try
            {
                var aptc = new MedicalExamCrudFactory();
                var lastMedicalExam = aptc.RetrieveMedicalExamByEmail<MedicalExam>(userEmail);
                return Ok(lastMedicalExam);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

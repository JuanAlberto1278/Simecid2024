using CoreApp;
using DataAccess.CRUD;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public ActionResult Create(Prescription prescription)
        {
            try
            {
                var pm = new PrescriptionManager();
                pm.Create(prescription);
                return Ok(prescription);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Prescription prescription)
        {
            try
            {
                var pm = new PrescriptionManager();
                pm.Update(prescription);
                return Ok(prescription);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(Prescription prescription)
        {
            try
            {
                var pm = new PrescriptionManager();
                pm.Delete(prescription);
                return Ok(prescription);
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
                var pm = new PrescriptionManager();
                var prescriptionList = pm.RetrieveAll();
                return Ok(prescriptionList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        [Route("RetrieveByEmail")]
        public ActionResult RetrievePrescriptionByEmail(string userEmail)
        {
            try
            {
                var aptc = new PrescriptionCrudFactory();
                var lastPrescription = aptc.RetrievePrescriptionByEmail<Prescription>(userEmail);
                return Ok(lastPrescription);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

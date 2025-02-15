using DataAccess.CRUD;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]

    public class DiagnosticController : ControllerBase
    {

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(Diagnostic diagnostic)
        {
            try
            {
                var dg = new DiagnosticCrudFactory();
                dg.Create(diagnostic);
                return Ok(diagnostic);
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
                var dg = new DiagnosticCrudFactory();
                var lastDiagnostic = dg.RetrieveAll<Diagnostic>();
                return Ok(lastDiagnostic);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Diagnostic diagnostic)
        {
            try
            {
                var dg = new DiagnosticCrudFactory();
                dg.Update(diagnostic);
                return Ok("Diagnostic updated successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(Diagnostic diagnostic)
        {
            try
            {
                var dg = new DiagnosticCrudFactory();
                dg.Delete(diagnostic);
                return Ok("Diagnostic deleted successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("RetrieveByEmail")]
        public ActionResult RetrieveDiagnosticByEmail(string userEmail)
        {
            try
            {
                var aptc = new DiagnosticCrudFactory();
                var lastDiagnostic = aptc.RetrieveDiagnosticByEmail<Diagnostic>(userEmail);
                return Ok(lastDiagnostic);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

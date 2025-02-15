using CoreApp;
using DataAccess.CRUD;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
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
                var um = new BranchManager();
                var branchList = um.RetrieveAll();
                return Ok(branchList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        [Route("RetrieveById")]
        public ActionResult RetrieveById(int brachId)
        {
            try
            {
                var um = new BranchManager();
                var branch = um.RetrieveById(brachId);

                if (branch != null)
                {
                    return Ok(branch);
                }
                else
                {
                    return NotFound("Branch not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost]
        [Route("Create")]
        public ActionResult Create(Branch branch)
        {
            try
            {
                var um = new BranchManager();
                um.Create(branch);
                return Ok(branch);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }



        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Branch branch)
        {
            try
            {
                var um = new BranchManager();
                um.Update(branch);
                return Ok(branch);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }



        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(Branch branch)
        {
            try
            {
                var um = new BranchManager();
                um.Delete(branch);
                return Ok(branch);


            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("AddServiceToBranch")]
        public ActionResult AddServiceToBranch(Branch branch)
        {
            try
            {
                var um = new BranchManager();

                // Verificar si el branch existe
                var existingBranch = um.RetrieveById(branch.Id);

                if (existingBranch == null)
                {
                    return NotFound("Branch not found");
                }

                // Agregar el servicio al branch
                um.AddServices(branch);

                return Ok(branch);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }





        [HttpGet]
        [Route("RetrieveAllServices")]
        public ActionResult RetrieveAllServices(int brachId)
        {
            try
            {
                var um = new BranchManager();
                var branch = um.RetrieveAllServices(brachId);
                var branchList = um.RetrieveAllServices(brachId);

                if (branch != null)
                {
                    return Ok(branch);
                }
                else
                {
                    return NotFound("Branch not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    

        [HttpGet]
        [Route("RetrieveAllBranchServices")]
        public ActionResult RetrieveAllBranchServices()
        {
            try
            {
                var um = new BranchManager();
                var branchList = um.RetrieveAllBranchServices();
                return Ok(branchList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }


    }

}


    


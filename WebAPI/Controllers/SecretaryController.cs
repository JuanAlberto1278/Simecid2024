using CoreApp;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
        [Route("api/[controller]")]
        [ApiController]

        public class SecretaryController : ControllerBase
        {
            [HttpGet]
            [Route("RetrieveAll")]
            public ActionResult RetrieveAll()
            {
                try
                {
                    var sm = new SecretaryManager();
                    var secretaryList = sm.RetrieveAll<Secretary>();
                    return Ok(secretaryList);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }

            }

            [HttpGet]
            [Route("RetrieveById")]
            public ActionResult RetrieveById(int Id)
            {
                try
                {
                    var sm = new SecretaryManager();
                    var secretary = sm.RetrieveById(Id);

                    if (secretary != null)
                    {
                        return Ok(secretary);
                    }
                    else
                    {
                        return NotFound("secretary not found");
                    }
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            [HttpPost]
            [Route("AddSchedule")]
            public ActionResult AddSchedule(Secretary secretary)
            {
                try
                {
                    var sm = new SecretaryManager();
                    sm.AddSchedule(secretary);
                    return Ok(secretary);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            [HttpPost]
            [Route("Create")]
            public ActionResult Create(Secretary secretary)
            {
                try
                {
                    var sm = new SecretaryManager();
                    sm.Create(secretary);
                    return Ok(secretary);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

            [HttpPut]
            [Route("Update")]
            public ActionResult Update(Secretary secretary)
            {
                try
                {
                    var sm = new SecretaryManager();
                    sm.Update(secretary);
                    return Ok(secretary);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }



            [HttpDelete]
            [Route("Delete")]
            public ActionResult Delete(Secretary secretary)
            {
                try
                {
                    var sm = new SecretaryManager();
                    sm.Delete(secretary);
                    return Ok(secretary);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, ex.Message);
                }
            }

        }
    }


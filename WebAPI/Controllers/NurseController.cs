using CoreApp;
using DTO;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NurseController : ControllerBase
	{
		[HttpGet]
		[Route("RetrieveAll")]
		public ActionResult RetrieveAll()
		{
			try
			{
				var um = new NurseManager();
				var nurseList = um.RetrieveAll();
				return Ok(nurseList);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}

		}

        [HttpPost]
        [Route("AddSchedule")]
        public ActionResult AddSchedule(Nurse nurse)
        {
            try
            {
                var nm = new NurseManager();
                nm.AddSchedule(nurse);
                return Ok(nurse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
		[Route("RetrieveById")]
		public ActionResult RetrieveById(int nurseId)
		{
			try
			{
				var um = new NurseManager();
				var nurse = um.RetrieveById(nurseId);

				if (nurse != null)
				{
					return Ok(nurse);
				}
				else
				{
					return NotFound("Nurse not found");
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}



		[HttpPost]
		[Route("Create")]
		public ActionResult Create(Nurse nurse)
		{
			try
			{
				var um = new NurseManager();
				um.Create(nurse);
				return Ok(nurse);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}



		}

		[HttpPut]
		[Route("Update")]
		public ActionResult Update(Nurse nurse)
		{
			try
			{
				var um = new NurseManager();
				um.Update(nurse);
				return Ok(nurse);
			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}

		}



		[HttpDelete]
		[Route("Delete")]
		public ActionResult Delete(Nurse nurse)
		{
			try
			{
				var um = new NurseManager();
				um.Delete(nurse);
				return Ok(nurse);


			}
			catch (Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}

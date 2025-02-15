using CoreApp;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DoctorController : ControllerBase
    {
        [HttpGet]
        [Route("RetrieveAll")]
        public ActionResult RetrieveAll()
        {
            try
            {
                var dm = new DoctorManager();
                var doctorList = dm.RetrieveAll<Doctor>();
                return Ok(doctorList);
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
                var dm = new DoctorManager();
                var doctor = dm.RetrieveById(Id);

                if (doctor != null)
                {
                    return Ok(doctor);
                }
                else
                {
                    return NotFound("Doctor not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("Create")]
        public ActionResult Create(Doctor doctor)
        {
            try
            {
                var dm = new DoctorManager();
                dm.Create(doctor);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("AddSpecialty")]
        public ActionResult AddSpecialty(Doctor doctor)
        {
            try
            {
                var dm = new DoctorManager();
                dm.AddSpecialty(doctor);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetSpecialtiesByBranch")]
        public ActionResult GetSpecialtiesByBranch(int branchId)
        {
            try
            {
                var doctorManager = new DoctorManager();
                var specialties = doctorManager.GetSpecialtiesByBranch(branchId);
                return Ok(specialties);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("AddSchedule")]
        public ActionResult AddSchedule(Doctor doctor)
        {
            try
            {
                var dm = new DoctorManager();
                dm.AddSchedule(doctor);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(Doctor doctor)
        {
            try
            {
                var dm = new DoctorManager();
                dm.Update(doctor);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(Doctor doctor)
        {
            try
            {
                var dm = new DoctorManager();
                dm.Delete(doctor);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut]
        [Route("UpdateDoctorSpecialty")]
        public ActionResult UpdateDoctorSpecialty(DoctorSpecialty doctorSpecialty)
        {
            try
            {
                var dm = new DoctorManager();
                dm.UpdateDoctorSpecialty(doctorSpecialty);
                return Ok(doctorSpecialty);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpGet]
        [Route("RetrieveDoctorSpecialty")]
        public ActionResult RetrieveDoctorSpecialty()
        {
            try
            {
                var dm = new DoctorManager();
                var doctorList = dm.RetrieveDoctorSpecialty<DoctorSpecialty>();
                return Ok(doctorList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}

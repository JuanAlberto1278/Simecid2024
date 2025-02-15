using CoreApp;
using DTO;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecoverPasswordController : ControllerBase
    {
        private readonly UserManager _userManager;

        public RecoverPasswordController(UserManager userManager)
        {
            _userManager = userManager;
        }


        [HttpPost]
        [Route("CreateData")]
        public ActionResult CreateOTP(ValidateOTP validateOTP)
        {
            try
            {
                var vm = new UserManager();
                vm.CreateOTP(validateOTP);
                return Ok(validateOTP);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }




        [HttpGet]
        [Route("VerifyOtp")]
        public IActionResult VerifyOtp()
        {
            try
            {
                var vm = new UserManager();
                var otpList = vm.RetrieveAllOTP();
                return Ok(otpList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult UpdatePassword(UpdatePassword request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (request.NewPassword != request.ConfirmPassword)
                {
                    throw new BadRequestException("Password don't match");
                }

                if (!_userManager.IsValidPassword(request.NewPassword))
                {
                    throw new BadRequestException("Password does not meet the requirements");
                }

                _userManager.UpdatePassword(request.Email, request.NewPassword, request.ConfirmPassword);

                return Ok(new { message = "Password changed successfully" + request.ConfirmPassword });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}

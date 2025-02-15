using CoreApp;
using DTO;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UserManager _userManager;

        public LoginController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        [Route("Authenticate")]
        public ActionResult Authenticate([FromBody] LoginRequest request)
        {
            try
            {
                bool isAuthenticated = _userManager.Authenticate(request.Email, request.Password);
                if (isAuthenticated)
                {
                    return Ok(new { message = "Login successful" });
                }
                else
                {
                    return Unauthorized(new { message = "Incorrect email or password" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
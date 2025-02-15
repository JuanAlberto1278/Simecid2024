using Microsoft.AspNetCore.Mvc;
using CoreApp;
namespace WebAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        [HttpPost]
        [Route("SendEmail")]
        public async Task<ActionResult> SendEmail([FromBody] EmailData data)
        {
            try
            {
                if (string.IsNullOrEmpty(data.EmailAddress))
                {
                    return BadRequest("Email address is required.");
                }

                var em = new EmailManager();

                return Ok(new { data.EmailAddress });
            }
            catch (FormatException ex)
            {
                return BadRequest("Invalid email address format.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while sending email.");
            }
        }
    }

    public class EmailData
    {
        public string EmailAddress { get; set; }
        public int OTP { get; set; }
    }

}

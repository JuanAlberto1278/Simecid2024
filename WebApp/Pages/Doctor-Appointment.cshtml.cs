using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages
{
    public class Doctor_AppointmentModel : PageModel
    {
        public void OnGet()
        {
        }

        public string Email { get; set; }

    }
}

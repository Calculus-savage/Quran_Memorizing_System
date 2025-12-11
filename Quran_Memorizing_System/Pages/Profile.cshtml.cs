using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Quran_Memorizing_System.Pages
{
    public class ProfileModel : PageModel
    {
        public string email { get; set; }
        public string role { get; set; }
        public string name { get; set; }
        public IActionResult OnGet()
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("email")))
            {
                return RedirectToPage("/Login_Page");
            }
            else
            {
                role = HttpContext.Session.GetString("role");
                email = HttpContext.Session.GetString("email");
                name = HttpContext.Session.GetString("name");
                return Page();
            }
        }
    }
}

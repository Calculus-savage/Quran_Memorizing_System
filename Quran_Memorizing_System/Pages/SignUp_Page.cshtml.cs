using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Quran_Memorizing_System.Models;

namespace Quran_Memorizing_System.Pages
{
    public class SignUp_Page : PageModel
    {
        [BindProperty]
        public User user { get; set; }
        
        private DB db;
        public SignUp_Page(DB DB)
        {
            db = DB;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            db.AddUser(user);
            return RedirectToPage("/Login_Page");
        }
    }
}

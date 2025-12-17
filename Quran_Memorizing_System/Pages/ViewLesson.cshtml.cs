using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Quran_Memorizing_System.Pages
{
    public class ViewLessonModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public LessonsSearchModel.Lesson Lesson { get; set; }

        public IActionResult OnGet()
        {
            Lesson = LessonsSearchModel.LessonsStore.FirstOrDefault(l => l.Id == Id);

            if (Lesson == null)
                return RedirectToPage("/LessonsSearch"); 

            return Page();
        }
    }
}

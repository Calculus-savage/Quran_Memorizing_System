using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Quran_Memorizing_System.Pages
{
    public class EditLessonModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public string Instructor { get; set; }

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string TagsString { get; set; }

        public IActionResult OnGet()
        {
            var lesson = LessonsSearchModel.LessonsStore.FirstOrDefault(l => l.Id == Id);
            if (lesson == null)
                return RedirectToPage("/LessonsSearch");

            Instructor = lesson.Instructor;
            Title = lesson.Title;
            TagsString = string.Join(", ", lesson.Tags);

            return Page();
        }

        public IActionResult OnPost()
        {
            var lesson = LessonsSearchModel.LessonsStore.FirstOrDefault(l => l.Id == Id);
            if (lesson == null)
                return RedirectToPage("/LessonsSearch");

            lesson.Instructor = Instructor;
            lesson.Title = Title;
            lesson.Tags = TagsString?.Split(',', System.StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList() ?? new List<string>();

            return RedirectToPage("/LessonsSearch");
        }
    }
}

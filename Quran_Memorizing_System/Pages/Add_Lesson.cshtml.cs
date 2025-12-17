using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Quran_Memorizing_System.Pages
{
    public class Add_LessonModel : PageModel
    {
        [BindProperty]
        public string Instructor { get; set; }

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string TagsString { get; set; } 

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Instructor) || string.IsNullOrWhiteSpace(Title))
                return Page();

            var mainPage = new LessonsSearchModel();
            var tags = TagsString?.Split(',', System.StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList() ?? new List<string>();

            
            int nextId = 1;
            if (LessonsSearchModel.LessonsStore.Any())
                nextId = LessonsSearchModel.LessonsStore.Max(l => l.Id) + 1;

            LessonsSearchModel.LessonsStore.Add(new LessonsSearchModel.Lesson
            {
                Id = nextId,
                Instructor = Instructor,
                Title = Title,
                Tags = tags
            });

            return RedirectToPage("/LessonsSearch");
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Quran_Memorizing_System.Pages
{
    public class LessonsSearchModel : PageModel
    {
        public static List<Lesson> LessonsStore = new List<Lesson>
        {
            new Lesson { Id = 1, Instructor = "Name", Title = "Lesson 1", Tags = new List<string> { "Tag1", "Tag2" } },
            new Lesson { Id = 2, Instructor = "Name", Title = "Lesson 2", Tags = new List<string> { "Tag1", "Tag2" } },
            new Lesson { Id = 3, Instructor = "Name", Title = "Lesson 3", Tags = new List<string> { "Tag1", "Tag2" } }
        };

        public List<Lesson> Lessons { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        public void OnGet()
        {
            if (!string.IsNullOrWhiteSpace(Search))
            {
                Lessons = LessonsStore
                    .Where(l => l.Title.Contains(Search, System.StringComparison.OrdinalIgnoreCase) ||
                                l.Instructor.Contains(Search, System.StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            else
            {
                Lessons = LessonsStore;
            }
        }

        public IActionResult OnPostDelete(int id)
        {
            var lesson = LessonsStore.FirstOrDefault(l => l.Id == id);
            if (lesson != null)
                LessonsStore.Remove(lesson);

            return RedirectToPage();
        }

        public class Lesson
        {
            public int Id { get; set; }
            public string Instructor { get; set; }
            public string Title { get; set; }
            public List<string> Tags { get; set; }
        }
    }
}

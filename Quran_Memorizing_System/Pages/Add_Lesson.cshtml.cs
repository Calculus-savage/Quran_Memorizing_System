using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;


namespace Quran_Memorizing_System.Pages
{
    public class Add_LessonModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public Add_LessonModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Location { get; set; }

        [BindProperty]
        public string Availability { get; set; }

        [BindProperty]
        public string Lesson_URL { get; set; }

        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated || User.IsInRole("Sheikh"))
                return RedirectToPage("/LessonsSearch");

            return Page();
        }

        public IActionResult OnPost()
        {
            if (User.Identity.IsAuthenticated || User.IsInRole("Sheikh"))
                return RedirectToPage("/LessonsSearch");

            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            string instructorEmail = User.Identity.Name; 

            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand(
                @"INSERT INTO Lessons
          (Title, Location, Availability, lesson_url, instrutor_email)
          VALUES
          (@Title, @Location, @Availability, @Url, @Instructor)", con);

            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@Location", Location);
            cmd.Parameters.AddWithValue("@Availability", Availability);
            cmd.Parameters.AddWithValue("@Url", Lesson_URL);
            cmd.Parameters.AddWithValue("@Instructor", instructorEmail);

            con.Open();
            cmd.ExecuteNonQuery();

            return RedirectToPage("/LessonsSearch");
        }
    }


    //public class Add_LessonModel : PageModel
    //{
    //    private readonly IConfiguration _configuration;

    //    public Add_LessonModel(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //    }

    //    [BindProperty] public int Lesson_ID { get; set; }
    //    [BindProperty] public string Title { get; set; }
    //    [BindProperty] public string Location { get; set; }
    //    [BindProperty] public string Availability { get; set; }

    //    public IActionResult OnPost()
    //    {
    //        string connStr = _configuration.GetConnectionString("DefaultConnection");

    //        using SqlConnection con = new SqlConnection(connStr);
    //        using SqlCommand cmd = new SqlCommand(@"
    //            INSERT INTO Lessons (Lesson_ID, Title, Location, Availability)
    //            VALUES (@id, @title, @location, @availability)", con);

    //        cmd.Parameters.AddWithValue("@id", Lesson_ID);
    //        cmd.Parameters.AddWithValue("@title", Title);
    //        cmd.Parameters.AddWithValue("@location", Location);
    //        cmd.Parameters.AddWithValue("@availability", Availability);

    //        con.Open();
    //        cmd.ExecuteNonQuery();

    //        return RedirectToPage("/LessonsSearch");
    //    }
    //}
    //public class Add_LessonModel : PageModel
    //{
    //    [BindProperty]
    //    public string Instructor { get; set; }

    //    [BindProperty]
    //    public string Title { get; set; }

    //    [BindProperty]
    //    public string TagsString { get; set; } 

    //    public void OnGet() { }

    //    public IActionResult OnPost()
    //    {
    //        if (string.IsNullOrWhiteSpace(Instructor) || string.IsNullOrWhiteSpace(Title))
    //            return Page();

    //        var mainPage = new LessonsSearchModel();
    //        var tags = TagsString?.Split(',', System.StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList() ?? new List<string>();


    //        int nextId = 1;
    //        if (LessonsSearchModel.LessonsStore.Any())
    //            nextId = LessonsSearchModel.LessonsStore.Max(l => l.Id) + 1;

    //        LessonsSearchModel.LessonsStore.Add(new LessonsSearchModel.Lesson
    //        {
    //            Id = nextId,
    //            Instructor = Instructor,
    //            Title = Title,
    //            Tags = tags
    //        });

    //        return RedirectToPage("/LessonsSearch");
    //    }
    //}
}

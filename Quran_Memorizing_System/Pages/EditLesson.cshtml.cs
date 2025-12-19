using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;


namespace Quran_Memorizing_System.Pages
{
    public class EditLessonModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public EditLessonModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Razor Pages binding for GET and POST
        [BindProperty(SupportsGet = true)]
        public int Lesson_ID { get; set; }

        [BindProperty]
        public string Title { get; set; } = "";

        [BindProperty]
        public string Location { get; set; } = "";

        [BindProperty]
        public string Availability { get; set; } = "";

        [BindProperty]
        public string LessonUrl { get; set; } = "";

        // GET method - fetch data from DB and populate the form
        public IActionResult OnGet()
        {
            string cs = _configuration.GetConnectionString("DefaultConnection");

            using SqlConnection con = new SqlConnection(cs);
            using SqlCommand cmd = new SqlCommand(
                @"SELECT Title, Location, Availability, lesson_url
              FROM Lessons
              WHERE Lesson_ID = @id", con);

            cmd.Parameters.AddWithValue("@id", Lesson_ID);

            con.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Title = reader["Title"].ToString();
                Location = reader["Location"].ToString();
                Availability = reader["Availability"].ToString();
                LessonUrl = reader["lesson_url"].ToString();
            }
            else
            {
                // If lesson not found, redirect to lessons list
                return RedirectToPage("/LessonsSearch");
            }

            return Page();
        }

        // POST method - update database with form data
        public IActionResult OnPost()
        {
            string cs = _configuration.GetConnectionString("DefaultConnection");

            using SqlConnection con = new SqlConnection(cs);
            using SqlCommand cmd = new SqlCommand(
                @"UPDATE Lessons
              SET Title = @t,
                  Location = @l,
                  Availability = @a,
                  lesson_url = @u
              WHERE Lesson_ID = @id", con);

            cmd.Parameters.AddWithValue("@t", Title);
            cmd.Parameters.AddWithValue("@l", Location);
            cmd.Parameters.AddWithValue("@a", Availability);
            cmd.Parameters.AddWithValue("@u", LessonUrl);
            cmd.Parameters.AddWithValue("@id", Lesson_ID);

            con.Open();
            cmd.ExecuteNonQuery();

            return RedirectToPage("/LessonsSearch");
        }
    }

    //public class EditLessonModel : PageModel
    //{
    //    private readonly IConfiguration _configuration;

    //    public EditLessonModel(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //    }

    //    [BindProperty(SupportsGet = true)]
    //    public int Lesson_ID { get; set; }

    //    [BindProperty] public string Title { get; set; }
    //    [BindProperty] public string Location { get; set; }
    //    [BindProperty] public string Availability { get; set; }

    //    public void OnGet()
    //    {
    //        string connStr = _configuration.GetConnectionString("DefaultConnection");

    //        using SqlConnection con = new SqlConnection(connStr);
    //        using SqlCommand cmd = new SqlCommand(@"
    //            SELECT * FROM Lessons WHERE Lesson_ID = @id", con);

    //        cmd.Parameters.AddWithValue("@id", Lesson_ID);

    //        con.Open();
    //        SqlDataReader r = cmd.ExecuteReader();

    //        if (r.Read())
    //        {
    //            Title = r["Title"].ToString();
    //            Location = r["Location"].ToString();
    //            Availability = r["Availability"].ToString();
    //        }
    //    }

    //    public IActionResult OnPost()
    //    {
    //        string connStr = _configuration.GetConnectionString("DefaultConnection");

    //        using SqlConnection con = new SqlConnection(connStr);
    //        using SqlCommand cmd = new SqlCommand(@"
    //            UPDATE Lessons
    //            SET Title = @title,
    //                Location = @location,
    //                Availability = @availability
    //            WHERE Lesson_ID = @id", con);

    //        cmd.Parameters.AddWithValue("@id", Lesson_ID);
    //        cmd.Parameters.AddWithValue("@title", Title);
    //        cmd.Parameters.AddWithValue("@location", Location);
    //        cmd.Parameters.AddWithValue("@availability", Availability);

    //        con.Open();
    //        cmd.ExecuteNonQuery();

    //        return RedirectToPage("/LessonsSearch");
    //    }


}
    //public class EditLessonModel : PageModel
    //{
    //    [BindProperty(SupportsGet = true)]
    //    public int Id { get; set; }

    //    [BindProperty]
    //    public string Instructor { get; set; }

    //    [BindProperty]
    //    public string Title { get; set; }

    //    [BindProperty]
    //    public string TagsString { get; set; }

    //    public IActionResult OnGet()
    //    {
    //        var lesson = LessonsSearchModel.LessonsStore.FirstOrDefault(l => l.Id == Id);
    //        if (lesson == null)
    //            return RedirectToPage("/LessonsSearch");

    //        Instructor = lesson.Instructor;
    //        Title = lesson.Title;
    //        TagsString = string.Join(", ", lesson.Tags);

    //        return Page();
    //    }

    //    public IActionResult OnPost()
    //    {
    //        var lesson = LessonsSearchModel.LessonsStore.FirstOrDefault(l => l.Id == Id);
    //        if (lesson == null)
    //            return RedirectToPage("/LessonsSearch");

    //        lesson.Instructor = Instructor;
    //        lesson.Title = Title;
    //        lesson.Tags = TagsString?.Split(',', System.StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToList() ?? new List<string>();

    //        return RedirectToPage("/LessonsSearch");
    //    }
    //}


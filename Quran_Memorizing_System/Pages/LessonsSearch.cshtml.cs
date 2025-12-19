using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Quran_Memorizing_System.Models;

namespace Quran_Memorizing_System.Pages
{
    public class LessonsSearchModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public List<Dictionary<string, object>> Lessons = new List<Dictionary<string, object>>();

        public LessonsSearchModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void OnGet()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("SELECT * FROM Lessons", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var lesson = new Dictionary<string, object>();
                lesson["Lesson_ID"] = reader["Lesson_ID"];
                lesson["Title"] = reader["Title"];
                lesson["Location"] = reader["Location"];
                lesson["Availability"] = reader["Availability"];
                lesson["instrutor_email"] = reader["instrutor_email"];
                lesson["lesson_url"] = reader["lesson_url"];
                Lessons.Add(lesson);
            }
        }

        public IActionResult OnPostDelete(int lessonId)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using SqlConnection con = new SqlConnection(connectionString);
            using SqlCommand cmd = new SqlCommand("DELETE FROM Lessons WHERE Lesson_ID=@id", con);
            cmd.Parameters.AddWithValue("@id", lessonId);
            con.Open();
            cmd.ExecuteNonQuery();
            return RedirectToPage("/LessonsSearch");
        }
    }
}

    //public class LessonsSearchModel : PageModel
    //{
    //    private readonly IConfiguration _configuration;

    //    public LessonsSearchModel(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //    }
    //    [BindProperty(SupportsGet = true)]
    //    public string SearchTerm { get; set; } = "";
    //    public List<LessonView> Lessons { get; set; } = new();

    //    public void OnGet()
    //    {
    //        string connectionString = _configuration.GetConnectionString("DefaultConnection");

    //        using SqlConnection con = new SqlConnection(connectionString);


    //        string query = "SELECT * FROM Lessons";
    //        if (!string.IsNullOrEmpty(SearchTerm))
    //        {
    //            query += " WHERE Title LIKE @search";
    //        }

    //        using SqlCommand cmd = new SqlCommand(query, con);

    //        if (!string.IsNullOrEmpty(SearchTerm))
    //        {
    //            cmd.Parameters.AddWithValue("@search", "%" + SearchTerm + "%");
    //        }

    //        con.Open();
    //        SqlDataReader reader = cmd.ExecuteReader();

    //        Lessons.Clear();
    //        while (reader.Read())
    //        {
    //            Lessons.Add(new LessonView
    //            {
    //                Lesson_ID = (int)reader["Lesson_ID"],
    //                Title = reader["Title"].ToString(),
    //                Location = reader["Location"].ToString(),
    //                Availability = reader["Availability"].ToString()
    //            });
    //        }
    //string connectionString = _configuration.GetConnectionString("DefaultConnection");

    //using SqlConnection con = new SqlConnection(connectionString);
    //using SqlCommand cmd = new SqlCommand("SELECT * FROM Lessons", con);

    //con.Open();
    //SqlDataReader reader = cmd.ExecuteReader();

    //while (reader.Read())
    //{
    //    Lessons.Add(new LessonView
    //    {
    //        Lesson_ID = (int)reader["Lesson_ID"],
    //        Title = reader["Title"].ToString(),
    //        Location = reader["Location"].ToString(),
    //        Availability = reader["Availability"].ToString()
    //    });
    //}


   

    //public class LessonsSearchModel : PageModel
    //{
    //    public static List<Lesson> LessonsStore = new List<Lesson>
    //    {
    //        new Lesson { Id = 1, Instructor = "Name", Title = "Lesson 1", Tags = new List<string> { "Tag1", "Tag2" } },
    //        new Lesson { Id = 2, Instructor = "Name", Title = "Lesson 2", Tags = new List<string> { "Tag1", "Tag2" } },
    //        new Lesson { Id = 3, Instructor = "Name", Title = "Lesson 3", Tags = new List<string> { "Tag1", "Tag2" } }
    //    };

    //    public List<Lesson> Lessons { get; set; }

    //    [BindProperty(SupportsGet = true)]
    //    public string Search { get; set; }

    //    public void OnGet()
    //    {
    //        if (!string.IsNullOrWhiteSpace(Search))
    //        {
    //            Lessons = LessonsStore
    //                .Where(l => l.Title.Contains(Search, System.StringComparison.OrdinalIgnoreCase) ||
    //                            l.Instructor.Contains(Search, System.StringComparison.OrdinalIgnoreCase))
    //                .ToList();
    //        }
    //        else
    //        {
    //            Lessons = LessonsStore;
    //        }
    //    }

    //    public IActionResult OnPostDelete(int id)
    //    {
    //        var lesson = LessonsStore.FirstOrDefault(l => l.Id == id);
    //        if (lesson != null)
    //            LessonsStore.Remove(lesson);

    //        return RedirectToPage();
    //    }

    //    public class Lesson
    //    {
    //        public int Id { get; set; }
    //        public string Instructor { get; set; }
    //        public string Title { get; set; }
    //        public List<string> Tags { get; set; }
    //    }
    //}


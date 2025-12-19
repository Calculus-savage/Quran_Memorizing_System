using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace Quran_Memorizing_System.Pages
{
    public class ViewLessonModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public ViewLessonModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Title = "";
        public string Location = "";
        public string Availability = "";
        public string lesson_url = "";
        public string instrutor_email = "";

        public void OnGet()
        {
            int id = int.Parse(Request.Query["id"]);
            string cs = _configuration.GetConnectionString("DefaultConnection");

            using SqlConnection con = new SqlConnection(cs);
            using SqlCommand cmd = new SqlCommand(
                @"SELECT Title, Location, Availability, lesson_url, instrutor_email 
                  FROM Lessons 
                  WHERE Lesson_ID = @id", con);

            cmd.Parameters.AddWithValue("@id", id);

            con.Open();
            using SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                Title = reader["Title"].ToString();
                Location = reader["Location"].ToString();
                Availability = reader["Availability"].ToString();
                lesson_url = reader["lesson_url"].ToString();
                instrutor_email = reader["instrutor_email"].ToString();
            }
        }
    }
    //public class ViewLessonModel : PageModel
    //{
    //    private readonly IConfiguration _configuration;

    //    public ViewLessonModel(IConfiguration configuration)
    //    {
    //        _configuration = configuration;
    //    }

    //    [BindProperty(SupportsGet = true)]
    //    public int Lesson_ID { get; set; }

    //    public LessonView Lesson { get; set; }

    //    public void OnGet()
    //    {
    //        string connStr = _configuration.GetConnectionString("DefaultConnection");

    //        using SqlConnection con = new SqlConnection(connStr);
    //        using SqlCommand cmd = new SqlCommand(@"
    //            SELECT * FROM Lessons WHERE Lesson_ID = @id", con);

    //        cmd.Parameters.AddWithValue("@id", Lesson_ID);

    //        con.Open();
    //        SqlDataReader reader = cmd.ExecuteReader();

    //        if (reader.Read())
    //        {
    //            Lesson = new LessonView
    //            {
    //                Lesson_ID = (int)reader["Lesson_ID"],
    //                Title = reader["Title"].ToString(),
    //                Location = reader["Location"].ToString(),
    //                Availability = reader["Availability"].ToString()
    //            };
    //        }
    //    }

    //    public class LessonView
    //    {
    //        public int Lesson_ID { get; set; }
    //        public string Title { get; set; }
    //        public string Location { get; set; }
    //        public string Availability { get; set; }
    //    }
    //}
}

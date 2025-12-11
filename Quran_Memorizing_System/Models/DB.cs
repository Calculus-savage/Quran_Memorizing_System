using System.Data;
using System.Data.SqlClient;

namespace Quran_Memorizing_System.Models
{
    public class DB
    {
        private string connectionstring;

        private SqlConnection con;

        public DB()
        {
            connectionstring = "Data Source=MAZEN\\SQLEXPRESS;Initial Catalog=MemorizationSystem;Integrated Security=True;";
            con = new SqlConnection(connectionstring);
        }

        public void AddUser(User user)
        {
            try
            {
                con.Open();

                string query = "";
                if (user.role == "Participant") {
                    query = "INSERT INTO Participants (Email, UserName, Password, Gender, Phone, Phonevisability, DateofBirth) VALUES (@Email, @UserName, @Passwordhased, @Gender, @Phone, @Phonevisiablity, @DateOfBirth)";
                }
                else { 
                    query = "INSERT INTO Sheikhs (Email, UserName, Password, Gender, Phone, Phonevisability, DateofBirth) VALUES (@Email, @UserName, @Passwordhased, @Gender, @Phone, @Phonevisiablity, @DateOfBirth)";
                }

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Email", user.Email.ToLower());
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                
                string hashedpassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
                cmd.Parameters.AddWithValue("@Passwordhased", hashedpassword);

                cmd.Parameters.AddWithValue("@Gender", user.gender[0]);
                cmd.Parameters.AddWithValue("@Phone", user.PhoneNumber);
                cmd.Parameters.AddWithValue("@Phonevisiablity", user.PhoneVisability);
                cmd.Parameters.AddWithValue("@DateOfBirth", user.DateOfBirth);

                Console.WriteLine(cmd.ToString());

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }


        public DataTable FindUser(string email, string password, string type)
        {
            string table = "";
            if (type == "Participant") { table = "Participants"; }
            else { table = "Sheikhs"; }
            string query = $"SELECT * FROM {table} WHERE Email = @Email";
            DataTable res = new DataTable();

            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email.ToLower());
                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                if (BCrypt.Net.BCrypt.Verify(password, Convert.ToString(dt.Rows[0]["Password"])))
                {
                    res = dt;
                }
            }
            catch
            {
            }
            finally
            {
                con.Close();
            }
            return res;
        }

        public bool EmailExists(string email, string type)
        {
            bool found = true;
            try
            {
                con.Open();
                string table = "";
                if (type == "Participant")
                {
                    table = "Participants";
                }
                else
                {
                    table = "Sheikhs";
                }

                string query = $"SELECT COUNT(*) FROM {table} WHERE Email=@email";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@email", email.ToLower());

                int res = Convert.ToInt32(cmd.ExecuteScalar());
                if (res == 0)
                    found = false;
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
            return found;
        }

        public bool PhoneExists(int phone, string type)
        {
            bool found = true;
            try
            {
                con.Open();
                string table = "";
                if (type == "Participant")
                {
                    table = "Participants";
                }
                else
                {
                    table = "Sheikhs";
                }

                string query = $"SELECT COUNT(*) FROM {table} WHERE Phone=@phone";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@phone", phone);

                int res = Convert.ToInt32(cmd.ExecuteScalar());
                if (res == 0)
                    found = false;
            }
            catch
            {

            }
            finally
            {
                con.Close();
            }
            return found;
        }
    }
}

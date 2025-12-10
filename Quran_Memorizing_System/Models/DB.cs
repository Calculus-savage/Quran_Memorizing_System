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

                cmd.Parameters.AddWithValue("@Email", user.Email);
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

    }
}

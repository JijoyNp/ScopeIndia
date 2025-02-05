using ScopeIndia.Models;
using Microsoft.Data.SqlClient;

namespace ScopeIndia.Data
{
    public class StudentClass:IStudent
    {
        private readonly string _DBConnect;
        public StudentClass(IConfiguration configuration)
        {
            _DBConnect = configuration.GetConnectionString("DBConnect");
        }
        public void Insert(StudentModel sm)
        {
            using (SqlConnection sqlConn = new SqlConnection(_DBConnect))
            { 
                sqlConn.Open();
                string insertQuery = "INSERT INTO StudentsTable(FirstName,LastName,Gender,DOB,Email,PhNo,Country,State,City,Hobbies,Avatar) VALUES(@FirstName,@LastName,@Gender,@DOB,@Email,@PhNo,@Country,@State,@City,@Hobbies,@Avatar)";
                using (SqlCommand cmd = new SqlCommand(insertQuery, sqlConn))
                {
               
                    cmd.Parameters.AddWithValue("@FirstName", sm.FirstName);
                    cmd.Parameters.AddWithValue("@LastName", sm.LastName);
                    cmd.Parameters.AddWithValue("@Gender", sm.Gender);
                    cmd.Parameters.AddWithValue("@DOB", sm.DOB);
                    cmd.Parameters.AddWithValue("@Email", sm.Email);
                    cmd.Parameters.AddWithValue("@PhNo", sm.PhNo);
                    cmd.Parameters.AddWithValue("@Country", sm.Country);
                    cmd.Parameters.AddWithValue("@State", sm.State);
                    cmd.Parameters.AddWithValue("@City", sm.City);
                    cmd.Parameters.AddWithValue("@Hobbies", sm.AllHobbies);
                    cmd.Parameters.AddWithValue("@Avatar", sm.Avatarpath);
                    cmd.ExecuteNonQuery();
                }
                sqlConn.Close();
            }
        }
    }
}

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
                string insertQuery = "INSERT INTO StudentsTable(FirstName,LastName,Gender,DOB,Email,PhNo,Country,State,City,Hobbies,AvatarPath,Password,CourseId) VALUES(@FirstName,@LastName,@Gender,@DOB,@Email,@PhNo,@Country,@State,@City,@Hobbies,@Avatar,@Password,@CourseId)";
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
                    cmd.Parameters.AddWithValue("@Password", sm.Password ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CourseId", sm.CourseId ?? (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
                sqlConn.Close();
            }
        }

        public void Update(StudentModel sm)
        {

            using (SqlConnection sqlConn = new SqlConnection(_DBConnect))
            {
                sqlConn.Open();
                string updateQuery = "UPDATE StudentsTable SET FirstName=@FirstName, LastName=@LastName, Gender=@Gender, DOB=@DOB, Email=@Email, PhNo=@PhNo, Country=@Country, State=@State, City=@City, Hobbies=@Hobbies, AvatarPath=@Avatar, Password=@Password , CourseId=@CourseId WHERE Id=@Id";

                using (SqlCommand cmd = new SqlCommand(updateQuery, sqlConn))
                {
                    cmd.Parameters.AddWithValue("@Id", sm.Id);
                    cmd.Parameters.AddWithValue("@FirstName", sm.FirstName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@LastName", sm.LastName ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Gender", sm.Gender ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DOB", sm.DOB ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", sm.Email ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@PhNo", sm.PhNo ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Country", sm.Country ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@State", sm.State  ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@City", sm.City ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Hobbies", sm.AllHobbies ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Avatar", sm.Avatarpath ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Password", sm.Password ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@CourseId", sm.CourseId ?? (object)DBNull.Value);  

                    cmd.ExecuteNonQuery();
                }
                sqlConn.Close();
            }
        }

        public StudentModel GetById(int Id)
        {
            StudentModel student = new StudentModel();

            using (SqlConnection conn = new SqlConnection(_DBConnect))
            {
                conn.Open();
                string selquery = "SELECT * FROM StudentsTable WHERE Id=@Id";
                using (SqlCommand cmd = new SqlCommand(selquery, conn))
                {
                    cmd.Parameters.AddWithValue("Id", Id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            student.Id = reader.GetInt32(0);
                            student.FirstName = reader.IsDBNull(1) ? null : reader.GetString(1);
                            student.LastName = reader.IsDBNull(2) ? null : reader.GetString(2);
                            student.Gender = reader.IsDBNull(3) ? null : reader.GetString(3);
                            student.DOB = reader.GetDateTime(4);
                            student.Email = reader.IsDBNull(5) ? null : reader.GetString(5);
                            student.PhNo = reader.IsDBNull(6) ? null : reader.GetString(6);
                            student.Country = reader.IsDBNull(7) ? null : reader.GetString(7);
                            student.State = reader.IsDBNull(8) ? null : reader.GetString(8);
                            student.City = reader.IsDBNull(9) ? null : reader.GetString(9);
                            student.AllHobbies = reader.IsDBNull(10) ? null : reader.GetString(10);
                            student.Avatarpath = reader.IsDBNull(11) ? null : reader.GetString(11);
                            student.Password= reader.IsDBNull(12) ? null : reader.GetString(12);
                            student.CourseId= reader.IsDBNull(13) ? null : reader.GetString(13);
                        }

                    }
                }
                conn.Close();
            }
            return student;
        }


        public StudentModel GetByEmail(string Email)
        {
            if (string.IsNullOrEmpty(Email))
                throw new ArgumentNullException(nameof(Email), "Email parameter is null or empty.");
            StudentModel student = new StudentModel();

            using (SqlConnection conn = new SqlConnection(_DBConnect))
            {
                conn.Open();
                string selquery = "SELECT * FROM StudentsTable WHERE Email=@Email";
                using (SqlCommand cmd = new SqlCommand(selquery, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", Email);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            student.Id = reader.GetInt32(0);
                            student.FirstName = reader.IsDBNull(1) ? null : reader.GetString(1);
                            student.LastName = reader.IsDBNull(2) ? null : reader.GetString(2);
                            student.Gender = reader.IsDBNull(3) ? null : reader.GetString(3);
                            student.DOB = reader.GetDateTime(4);
                            student.Email = reader.IsDBNull(5) ? null : reader.GetString(5);
                            student.PhNo = reader.IsDBNull(6) ? null : reader.GetString(6);
                            student.Country = reader.IsDBNull(7) ? null : reader.GetString(7);
                            student.State = reader.IsDBNull(8) ? null : reader.GetString(8);
                            student.City = reader.IsDBNull(9) ? null : reader.GetString(9);
                            student.AllHobbies = reader.IsDBNull(10) ? null : reader.GetString(10);
                            student.Avatarpath = reader.IsDBNull(11) ? null : reader.GetString(11);
                            student.Password = reader.IsDBNull(12) ? null : reader.GetString(12);
                            student.CourseId = reader.IsDBNull(13) ? null : reader.GetString(13);

                        }
                    }
                    conn.Close();
                }
                return student;
            }
        }
        public List<StudentModel> GetAll()
        {
            List<StudentModel> student = new List<StudentModel>();

            using (SqlConnection conn = new SqlConnection(_DBConnect))
            {
                conn.Open();
                string selquery = "SELECT * FROM StudentsTable";
                using (SqlCommand cmd = new SqlCommand(selquery, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            student.Add(new StudentModel
                            {
                                Id = reader.GetInt32(0),
                                FirstName = reader.IsDBNull(1) ? null : reader.GetString(1),
                                LastName = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Gender = reader.IsDBNull(3) ? null : reader.GetString(3),
                                DOB = reader.GetDateTime(4),
                                Email = reader.IsDBNull(5) ? null : reader.GetString(5),
                                PhNo = reader.IsDBNull(6) ? null : reader.GetString(6),
                                Country = reader.IsDBNull(7) ? null : reader.GetString(7),
                                State = reader.IsDBNull(8) ? null : reader.GetString(8),
                                City = reader.IsDBNull(9) ? null : reader.GetString(9),
                                AllHobbies = reader.IsDBNull(10) ? null : reader.GetString(10),
                                Avatarpath = reader.IsDBNull(11) ? null : reader.GetString(11),
                                Password = reader.IsDBNull(12) ? null : reader.GetString(12),
                                CourseId = reader.IsDBNull(13) ? null : reader.GetString(13),




                            });
                        }
                    }
                }
                conn.Close();
            }
            return student;
        }

        public bool IsEmailExists(string email)
        {
            bool exists = false;

            using (SqlConnection conn = new SqlConnection(_DBConnect))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM studentstable WHERE Email = @Email";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    exists = (int)cmd.ExecuteScalar() > 0; // If count > 0, email exists
                }
            }
            return exists;
        }
    }
}

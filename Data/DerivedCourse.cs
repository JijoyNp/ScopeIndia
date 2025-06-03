using Microsoft.Data.SqlClient;
using ScopeIndia.Models;
using Dapper;

namespace ScopeIndia.Data
{
    public class DerivedCourse : ICourse
    {
        private readonly string _DBConnect;

        public DerivedCourse(IConfiguration configuration)
        {
            _DBConnect = configuration.GetConnectionString("DBConnect");
        }

        public List<CourseModel> GetAll()
        {
            List<CourseModel> student = new List<CourseModel>();

            using (SqlConnection conn = new SqlConnection(_DBConnect))
            {
                conn.Open();
                string selquery = "SELECT * FROM CourseTable";
                using (SqlCommand cmd = new SqlCommand(selquery, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            student.Add(new CourseModel
                            {
                                CourseId = reader.GetInt32(0),
                                CourseName = reader.IsDBNull(1) ? null : reader.GetString(1),
                                CourseDuration = reader.IsDBNull(2) ? null : reader.GetString(2),
                                CourseFee =  reader.GetInt32(3),
                            });
                            
                        }
                    }
                }
                conn.Close();
            }
            return student;
        }

    }
}

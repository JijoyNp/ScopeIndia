using Microsoft.Data.SqlClient;
using ScopeIndia.Models;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ScopeIndia.Data
{
    public class DerivedCourse : ICourse
    {
        private readonly string _DBConnect;
        private readonly object _context;

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

        public CourseModel GetById(int courseId)
        {
            CourseModel course = null;
            string query = "SELECT * FROM CourseTable WHERE CourseId = @CourseId"; // Use CourseId instead of Id

            using (SqlConnection con = new SqlConnection(_DBConnect))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@CourseId", courseId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            course = new CourseModel
                            {
                                CourseId = Convert.ToInt32(reader["CourseId"]),  // Use CourseId
                                CourseName = reader["CourseName"].ToString(),
                                CourseDuration = reader["CourseDuration"].ToString(),
                                CourseFee = Convert.ToInt32(reader["CourseFee"])
                            };
                        }
                    }
                }
            }
            return course;
        }

        public void AddCourseToStudent(int studentId, int courseId)
        {
            using (SqlConnection conn = new SqlConnection(_DBConnect))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO StudentCoursesTable (StudentId, CourseId) VALUES (@StudentId, @CourseId)", conn);
                cmd.Parameters.AddWithValue("@StudentId", studentId);
                cmd.Parameters.AddWithValue("@CourseId", courseId);
                cmd.ExecuteNonQuery();
            }
        }

        public List<CourseModel> GetSelectedCourses(int studentId)
        {
            List<CourseModel> selectedCourses = new List<CourseModel>();

            using (SqlConnection conn = new SqlConnection(_DBConnect))
            {
                conn.Open();
                string query = @"
            SELECT C.CourseId, C.CourseName, C.CourseDuration, C.CourseFee
            FROM StudentCoursesTable SC
            INNER JOIN CourseTable C ON SC.CourseId = C.CourseId
            WHERE SC.StudentId = @StudentId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentId", studentId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CourseModel course = new CourseModel
                            {
                                CourseId = Convert.ToInt32(reader["CourseId"]),
                                CourseName = reader["CourseName"].ToString(),
                                CourseDuration = reader["CourseDuration"].ToString(),
                                CourseFee = Convert.ToInt32(reader["CourseFee"])
                            };
                            selectedCourses.Add(course);
                        }
                    }
                }
            }

            return selectedCourses;
        }

        public int GetStudentCourseCount(int studentId)
        {
            int count = 0;

            using (SqlConnection conn = new SqlConnection(_DBConnect))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM StudentCoursesTable WHERE StudentId = @StudentId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StudentId", studentId);
                    count = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return count;
        }

    }
}

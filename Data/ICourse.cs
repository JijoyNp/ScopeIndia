using ScopeIndia.Models;

namespace ScopeIndia.Data
{
    public interface ICourse
    {
        List<CourseModel> GetAll();
        CourseModel GetById(int id);
        List<CourseModel> GetSelectedCourses(int studentId);
        void AddCourseToStudent(int studentId, int courseId);
        int GetStudentCourseCount(int studentId);

    }
}

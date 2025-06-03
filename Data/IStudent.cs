using ScopeIndia.Models;

namespace ScopeIndia.Data
{
    public interface IStudent
    {
        public void Insert(StudentModel sm);
        public void Update(StudentModel sm);

        public StudentModel GetById(int Id);

        public StudentModel GetByEmail(string Email);

        public List<StudentModel> GetAll();

        public bool IsEmailExists(string email);
    }
}

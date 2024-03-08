using ELibrary.Domain.Models;
using System.Linq.Expressions;

namespace E_LibraryApi.Repository.IRepository
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllStudents();
        Task<Student> GetStudent(Expression<Func<Student, bool>> filter);
        Task<bool> StudentExists(string studentName);
        Task CreateStudent(Student student);
        Task UpdateStudent(Student student);
        Task DeleteStudent(Student studentId);
        Task Save();
    }
}

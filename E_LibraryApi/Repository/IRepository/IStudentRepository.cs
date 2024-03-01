using E_LibraryApi.Models;
using E_LibraryApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace E_LibraryApi.Repository.IRepository
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllStudents();
        Task<Student> GetStudent(Guid studentId);
        Task<bool> StudentExists(string studentName);
        Task CreateStudent(Student student);
        Task UpdateStudent(Student student);
        Task DeleteStudent(Guid studentId);
        Task Save();
    }
}

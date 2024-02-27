using E_LibraryApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace E_LibraryApi.Repository.IRepository
{
    public interface IStudentRepository
    {
        Task<List<StudentDto>> GetAllStudents();
        Task<StudentDto> GetStudent(Guid studentId);
        Task<bool> StudentExists(string studentName);
        Task CreateStudent(StudentDto student);
        Task UpdateStudent(StudentDto student);
        Task DeleteStudent(Guid studentId);
        Task Save();
    }
}

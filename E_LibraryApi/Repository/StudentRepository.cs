using E_LibraryApi.Repository.IRepository;
using E_LibraryManagementSystem.Db;
using ELibrary.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace E_LibraryApi.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly E_LibDb db;

        public StudentRepository(E_LibDb db)
        {
            this.db = db;
        }
        public async Task CreateStudent(Student student)
        {
            await db.Student.AddAsync(student);
           await Save();
        }

        public async Task DeleteStudent(Student student)
        {
            db.Student.Remove(student);
            await Save();
        }

        

        public  async Task<List<Student>> GetAllStudents()
        {
            return await db.Student.ToListAsync();
        }

        public async Task<Student> GetStudent(Expression<Func<Student, bool>> filter)
        {
            IQueryable<Student> student = db.Student;
            student = student.Where(filter);
            return await student.FirstOrDefaultAsync();
        }

        public async Task Save()
        {
             await db.SaveChangesAsync();
        }

        public async Task<bool> StudentExists(string studentName)
        {
            var student= await db.Student.FirstOrDefaultAsync(s=>s.StudentName==studentName);
                return student!=null;
        }

        public async Task UpdateStudent(Student student)
        {
           db.Student.Update(student);
            await Save();
        }
    }
}

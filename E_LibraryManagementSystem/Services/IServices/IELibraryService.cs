using E_LibraryManagementSystem.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LibraryManagementSystem.Services.IServices
{
    public interface IELibraryService:IService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetByIdAsync<T>(Guid id);
       /* Task<T> GetByEnrollmentNbAsync<T>(int enrollment);
        Task<T> GetByStudentAsync<T>(int enrollment);*/

        Task<T> CreateAsync<T>(StudentDto obj);
        Task<T> UpdateAsync<T>(Guid id, StudentDto obj);
        Task<T> DeleteAsync<T>(Guid id);
        
    }
}

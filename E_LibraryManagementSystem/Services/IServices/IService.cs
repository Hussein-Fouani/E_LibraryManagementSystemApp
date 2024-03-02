using E_LibraryManagementSystem.Models;
using E_LibraryManagementSystem.Models.APIResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LibraryManagementSystem.Services.IServices
{
    public interface IService
    {
        ApiReponse responseModel { get; set; } 
        Task<T> SendAsync<T>(ApiRequests apiRequest);
    }
}

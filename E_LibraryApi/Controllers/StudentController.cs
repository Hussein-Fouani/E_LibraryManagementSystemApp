using AutoMapper;
using E_LibraryApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly StudentRepository studentRepository;

        public StudentController(IMapper mapper,StudentRepository studentRepository)
        {
            this.mapper = mapper;
            this.studentRepository = studentRepository;
        }
    }
}

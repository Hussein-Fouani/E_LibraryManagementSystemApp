using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMapper mapper;

        public StudentController(IMapper mapper)
        {
            this.mapper = mapper;
        }
    }
}

using AutoMapper;
using E_LibraryApi.Models;
using E_LibraryApi.Models.APIResponse;
using E_LibraryApi.Models.Dto;
using E_LibraryApi.Repository;
using E_LibraryApi.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace E_LibraryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IStudentRepository studentRepository;
        protected ApiReponse apiResponse;

        public StudentController(IMapper mapper, IStudentRepository studentRepository)
        {
            this.mapper = mapper;
            this.studentRepository = studentRepository;
            this.apiResponse = new ApiReponse();
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<ApiReponse>> CreateStudent([FromBody] StudentDto studentDto)
        {
            try
            {
                if (studentDto == null || !ModelState.IsValid)
                {
                    apiResponse.StatusCode = HttpStatusCode.BadRequest;
                    apiResponse.Result = new ApiReponse();
                    apiResponse.IsSuccess = false;
                    return BadRequest(apiResponse);
                }
                if (await studentRepository.StudentExists(studentDto.StudentName))
                {
                    ModelState.AddModelError("StudentName", "Student with this name already exists.");
                    apiResponse.StatusCode = HttpStatusCode.Conflict;
                    return BadRequest(ModelState);
                }
                var student = mapper.Map<Student>(studentDto);
                await studentRepository.CreateStudent(student);

                apiResponse.Result = mapper.Map<StudentDto>(student);
                apiResponse.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute(nameof(GetStudentById), new { Id = student.Id }, apiResponse);
            }
            catch (Exception)
            {
                apiResponse.IsSuccess = false;
                apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return apiResponse;
            }
        }
        [HttpGet("{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiReponse>> GetStudentById(Guid Id)
        {
            try
            {
                if (Id == Guid.Empty)
                {
                    ModelState.AddModelError(nameof(Id), "ID is not valid.");
                    return BadRequest(ModelState);
                }

                var student = await studentRepository.GetStudent(s => s.Id == Id);

                if (student == null)
                {
                    ModelState.AddModelError(nameof(Id), "Student not found.");
                    return NotFound(ModelState);
                }

                apiResponse.Result = mapper.Map<StudentDto>(student);
                apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(apiResponse);
            }
            catch (Exception)
            {
                apiResponse.Result = new ApiReponse();
                apiResponse.IsSuccess = false;
                apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return apiResponse;
            }
        }

        [HttpGet("{studentname:alpha}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiReponse>> GetStudentByName(string studentname)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(studentname))
                {
                    ModelState.AddModelError(nameof(studentname), "Student name is required.");
                    return BadRequest(ModelState);
                }

                var student = await studentRepository.GetStudent(s => s.StudentName == studentname);

                if (student == null)
                {
                    ModelState.AddModelError(nameof(studentname), "Student not found.");
                    return NotFound(ModelState);
                }

                apiResponse.Result = mapper.Map<StudentDto>(student);
                apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(apiResponse);
            }
            catch (Exception)
            {
                apiResponse.IsSuccess = false;
                apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return apiResponse;
            }
        }


        [HttpGet("{enrollmentNb:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ApiReponse>> GetStudentByEnrollmentNb(int enrollmentNb)
        {
            try
            {
                if (enrollmentNb < 0)
                {
                    ModelState.AddModelError(nameof(enrollmentNb), "Enrollment Number is not valid.");
                    return BadRequest(ModelState);
                }

                var student = await studentRepository.GetStudent(s => s.EnrollmentNb == enrollmentNb);

                if (student == null)
                {
                    ModelState.AddModelError(nameof(enrollmentNb), "Student not found.");
                    return NotFound(ModelState);
                }

                apiResponse.Result = mapper.Map<StudentDto>(student);
                apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(apiResponse);
            }
            catch (Exception)
            {
                apiResponse.IsSuccess = false;
                apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return apiResponse;
            }
        }




        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiReponse>> GetAllStudents()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model state");
                }
                var students = await studentRepository.GetAllStudents();
                if (students == null || students.Count <= 0)
                {
                    apiResponse.Result = new ApiReponse();
                    apiResponse.IsSuccess = false;
                    apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return BadRequest("No Students Found");

                }
                apiResponse.Result = mapper.Map<List<StudentDto>>(students);
                apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                apiResponse.IsSuccess = false;
                apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return apiResponse;
            }

        }

        [HttpDelete("{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ApiReponse>> DeleteStudent(Guid Id)
        {
            try
            {
                if (Id == Guid.Empty || !ModelState.IsValid)
                {
                    apiResponse.Result = new ApiReponse();
                    apiResponse.IsSuccess = false;
                    apiResponse.StatusCode = Id == Guid.Empty ? HttpStatusCode.BadRequest : HttpStatusCode.NotFound;
                    return BadRequest(apiResponse);
                }
                var student = await studentRepository.GetStudent(s => s.Id == Id);
                if (student == null)
                {
                    apiResponse.Result = new ApiReponse();
                    apiResponse.IsSuccess = false;
                    apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(apiResponse);
                }
                await studentRepository.DeleteStudent(student);
                apiResponse.IsSuccess = true;
                apiResponse.Result = new ApiReponse();
                return Ok(apiResponse);
            }
            catch (Exception)
            {

                apiResponse.IsSuccess = false;
                apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return apiResponse;
            }
        }
        [HttpPut("{Id:guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ApiReponse>> UpdateStudent(Guid Id, [FromBody] StudentDto updatedStudentDto)
        {
            try
            {
                if (Id == Guid.Empty || updatedStudentDto == null || !ModelState.IsValid)
                {
                    apiResponse.Result = new ApiReponse();
                    apiResponse.IsSuccess = false;
                    apiResponse.StatusCode = Id == Guid.Empty ? HttpStatusCode.BadRequest : HttpStatusCode.NotFound;
                    return BadRequest(apiResponse);
                }

                var existingStudent = await studentRepository.GetStudent(s => s.Id == Id);

                if (existingStudent == null)
                {
                    apiResponse.Result = new ApiReponse();
                    apiResponse.IsSuccess = false;
                    apiResponse.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(apiResponse);
                }
                existingStudent.StudentName = updatedStudentDto.StudentName;
                existingStudent.Department = updatedStudentDto.Department;
                existingStudent.StudentSemester = updatedStudentDto.StudentSemester;
                existingStudent.StudentContact = updatedStudentDto.StudentContact;
                existingStudent.EnrollmentNb = updatedStudentDto.EnrollmentNb;
                existingStudent.StudentEmail = updatedStudentDto.StudentEmail;




                await studentRepository.UpdateStudent(existingStudent);

                apiResponse.Result = mapper.Map<StudentDto>(existingStudent);
                apiResponse.IsSuccess = true;
                apiResponse.StatusCode = HttpStatusCode.Accepted;
                return Accepted(apiResponse);
            }
            catch (Exception)
            {
                apiResponse.IsSuccess = false;
                apiResponse.StatusCode = HttpStatusCode.InternalServerError;
                return apiResponse;
            }
        }


    }


}


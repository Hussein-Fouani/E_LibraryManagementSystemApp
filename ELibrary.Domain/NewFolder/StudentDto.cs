using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Domain.NewFolder
{
    public class StudentDto
    {

        public Guid Id { get; set; }
        [Required(ErrorMessage = "Student Name is required.")]
        [MaxLength(50)]
        [MinLength(5)]
        [DataType(DataType.Text)]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Student Email is required.")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        [MinLength(20)]
        public string StudentEmail { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        [MaxLength(50)]
        [MinLength(5)]
        [DataType(DataType.Text)]
        public string Department { get; set; }

        [Required(ErrorMessage = "Enrollment Number is required.")]
        public int EnrollmentNb { get; set; }

        [Required(ErrorMessage = "Student Semester is required.")]
        [MaxLength(7)]
        [MinLength(3)]
        public string StudentSemester { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Student Contact is required.")]
        public int StudentContact { get; set; }

        [MaxLength(15)]
        [MinLength(6)]
        public string? BookName { get; set; }

        [DataType(DataType.Date)]
        public DateTime BookIssueDate { get; set; }
    }
}

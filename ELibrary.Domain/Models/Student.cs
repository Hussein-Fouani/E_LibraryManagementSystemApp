 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Domain.Models
{
    public class Student:DomainObject
    {
        [Required(ErrorMessage = "Student Name is required.")]
        [MaxLength(50)]
        [MinLength(3)]
        public string StudentName { get; set; }

        [Required(ErrorMessage = "Student Email is required.")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        [MinLength(20)]
        public string StudentEmail { get; set; }

        [Required(ErrorMessage = "Department is required.")]
        [MaxLength(10)]
        [MinLength(3)]
        public string Department { get; set; }

        [Required(ErrorMessage = "Enrollment Number is required.")]
        public int EnrollmentNb { get; set; }

        [Required(ErrorMessage = "Student Semester is required.")]
        [MaxLength(8)]
        [MinLength(3)]
        public string StudentSemester { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "This field is required.")]
        public int StudentContact { get; set; }
        [MaxLength(15)]
        [MinLength(6)]
        public string? BookName { get; set; }

        
    }
}

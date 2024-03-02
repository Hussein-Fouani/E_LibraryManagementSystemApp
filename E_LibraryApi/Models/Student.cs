using System.ComponentModel.DataAnnotations;

namespace E_LibraryApi.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string StudentName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        [MinLength(20)]
        public string StudentEmail { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Department { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int EnrollmentNb { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string StudentSemester { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "This field is required.")]
        public int StudentContact { get; set; }

       // public BookModel book { get; set; }

    }
}

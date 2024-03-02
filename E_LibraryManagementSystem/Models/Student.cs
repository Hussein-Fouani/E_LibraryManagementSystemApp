using System.ComponentModel.DataAnnotations;

namespace E_LibraryManagementSystem.Models
{
    public class Student
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(50)]
        [MinLength(5)]
        public string StudentName { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        [MinLength(20)]
        public string StudentEmail { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(50)]
        [MinLength(5)]
        public string Department { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [Range(5, 10, ErrorMessage = "The value must be between 5 and 10    .")]
        public int EnrollmentNb { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [MaxLength(20)]
        [MinLength(5)]
        public string StudentSemester { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Range(8, 12, ErrorMessage = "The value must be between 8 and 12.")]
        [Required(ErrorMessage = "This field is required.")]
        public int StudentContact { get; set; }


        // public BookModel book { get; set; }

    }
}

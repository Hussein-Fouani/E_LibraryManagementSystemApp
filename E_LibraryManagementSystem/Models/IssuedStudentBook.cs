using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_LibraryManagementSystem.Models
{
    public class IssuedStudentBook
    {
        [Key]
        public Guid IssuedBookId { get; set; }
        [ForeignKey("BookId")]
        public Guid BookId { get; set; }
        [ForeignKey("StudentId")]
        public Guid StudentId { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime IssueDate { get; set; }
        [DataType(DataType.Date)]
        [Required]
        public DateTime ReturnDate { get; set; }
        public Student student { get; set; }
        public BookModel book { get; set; }
    }
}

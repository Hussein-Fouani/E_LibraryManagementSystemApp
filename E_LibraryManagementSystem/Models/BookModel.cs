using System.ComponentModel.DataAnnotations;

namespace E_LibraryManagementSystem.Models
{
    public class BookModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string BookName { get; set; }
        [Required]
        [StringLength(100)]
        public string  BookAuthor { get; set; }
        [Required]
        [StringLength(100)]
        public string BookPublication { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double BookPrice { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime BookPurhcaseDate { get; set; }
        [Required]
        [Range(1,5)]
        public int BookQuantity { get; set; }


       // public List<Student> students { get; set; }

    }
}

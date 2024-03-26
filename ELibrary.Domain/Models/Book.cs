using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Domain.Models
{
    public class Book:DomainObject
    {

        
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string BookName { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string BookAuthor { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string BookPublication { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(10)]
        public string ISBN { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(4)]
        public string Language { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public double BookPrice { get; set; }
        [Required]
        public bool IsAvailable { get; set; } = true;
        [MaxLength(100)]
        [MinLength(5)]
        public string Genre { get; set; }
        public  int  NumberOfCopies { get; set; }
        public virtual ICollection<BorrowedBooks> BorrowedBooks { get; set; }
    }
}

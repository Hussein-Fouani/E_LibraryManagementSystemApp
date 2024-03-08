using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Domain.Models
{
    public class BorrowedBooks
    {
        [Key]
        public Guid BorrowBookId { get; set; }
        
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        [ForeignKey("BookId")]
        public Guid BookId { get; set; }
        public string BorrowDate { get; set; }
        public string? ReturnDate { get; set; }
        public virtual UserRL User { get; set; }
        public virtual Book Book { get; set; }
    }

}

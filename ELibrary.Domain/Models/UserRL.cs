using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Domain.Models
{
    public class UserRL
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        [MaxLength(10)]
        [MinLength(4)]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Email { get; set; }
        public virtual ICollection<BorrowedBooks> BorrowedBooks { get; set; }


    }
}

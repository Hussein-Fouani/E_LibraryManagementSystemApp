using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [StringLength(30, ErrorMessage = "Username must be at least {2} characters long.", MinimumLength = 4)]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Password must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Email { get; set; }
        public virtual ICollection<BorrowedBooks> BorrowedBooks { get; set; }
        [ForeignKey("UserRole")]
        public string Role { get; set; }
        public virtual UsersRole UserRole { get; set; }


    }
}

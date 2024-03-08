using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Username Must be less than 4")]
        [MaxLength(15)]
        [MinLength(4)]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Name Must not be less than 4")]
        [MaxLength(15)]
        [MinLength(4)]
        [DataType(DataType.Text)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Password Must not be less than 8")]
        [MinLength(8)]
        [DataType(DataType.Text)]
        public string Password { get; set; }
        public string Role { get; set; }

    }
}

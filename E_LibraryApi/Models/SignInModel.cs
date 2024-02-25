using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_LibraryManagementSystem.Models
{
    public class SignInModel
    {
        public Guid Id { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(20)]
        public string UserName { get; set; }
        [Required]
        [MinLength(4)]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}

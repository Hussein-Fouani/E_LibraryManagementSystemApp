using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ELibrary.Domain.Models
{
    public class UsersRole
    {
        
        public string Role { get; set; }
        [Key]
        public string UserName { get; set; }
        public virtual ICollection<UserRL> Users { get; set; }
    }
}

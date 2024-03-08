using System.ComponentModel.DataAnnotations;

namespace E_LibraryApi.Models
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
        [Required]
        [MaxLength(20)]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}

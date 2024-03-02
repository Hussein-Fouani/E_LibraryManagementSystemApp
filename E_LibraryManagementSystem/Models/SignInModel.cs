using System.ComponentModel.DataAnnotations;


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

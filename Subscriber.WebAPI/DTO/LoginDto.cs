using System.ComponentModel.DataAnnotations;

namespace Subscriber.WebAPI.DTO
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
     public string Email { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(15)]
        public string Password { get; set; }
    } 
}
  
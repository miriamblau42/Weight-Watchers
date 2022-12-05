using System.ComponentModel.DataAnnotations;

namespace Subscriber.WebAPI.DTO
{
    public class RegisterSubscriberDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [MinLength(8)]
        [MaxLength(15)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        //[Range(0,300)]
        public float Height { get; set; }
    }
}

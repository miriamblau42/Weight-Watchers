using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subscriber.Data.Entities
{
    [Index("Email", IsUnique = true)]
    public class Subscriber
    {
        //[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]

        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.Data.Entities
{
    public class EmailVerification
    {
        [Key]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(4)]
        public string VerificationCode { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime ExpirationTime { get; set; }
    }
}

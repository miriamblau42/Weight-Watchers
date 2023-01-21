using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.Data.Entities;

[Index("Email",IsUnique = true)]
public class Subscriber
{
    [Required]
    [Key]
    public string Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    [MinLength(6)]
    public string Password { get; set; }
}

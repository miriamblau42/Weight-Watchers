using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Subscriber.Data.Entities;

public class Card
{
    [Required]
    [Key]
    public int Id { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime OpenDate { get; set; }
    [DataType(DataType.Date)]
    public DateTime? UpdateDate { get; set; }
    [Required]
    public float BMI { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
    [Required]
    public string SubscriberId { get; set; }
    [ForeignKey("SubscriberId")]
    public virtual Subscriber Subscriber { get; set; }
}

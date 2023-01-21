using Subscriber.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Measure.Data.Entities;
public enum EStatus
{
    InProcess, Success, Failed
}
public class Measure
{
    [Required]
    [Key]
    public int Id { get; set; }
    [Required]
    public int CardId { get; set; }
    [ForeignKey("CardId")]
    public virtual Card Card { get; set; }
    public float Weight { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    public EStatus Status { get; set; }
    public string? Comment { get; set; }
}

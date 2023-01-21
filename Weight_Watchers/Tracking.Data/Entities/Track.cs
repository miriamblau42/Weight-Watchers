using Subscriber.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tracking.Data.Entities;

public enum ETrend { Increase, Decrease, Static }

public class Track
{
    [Required]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public int CardId { get; set; }
    public float BMI { get; set; }
    public float Weight { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime Date { get; set; }
    public ETrend Trend { get; set; }
    public string? Comment { get; set; }
}

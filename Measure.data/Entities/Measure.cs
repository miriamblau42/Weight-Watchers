using System.ComponentModel.DataAnnotations;

namespace Measure.Data.Entities
{
    public enum Status
    {
        InProgress=0, Succsesed=1, Failed=2
    }
    public class Measure
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int CardID { get; set; }
        [Required]
        public float Weight { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        [Required]
        public Status Status { get; set; }
        public string? Comments { get; set; }
        
    }
}

using System.ComponentModel.DataAnnotations;

namespace Measure.WebAPI.DTO
{
    public class MeasurePostDTO
    {
        [Required]
        public int CardID { get; set; }
        [Required]
        public float Weight { get; set; }
        public string? Comments { get; set; }

    }
}

using Measure.Data.Entities;

namespace Measure.WebApi.DTO;

public class PostMeasureDTO
{
    public int CardId { get; set; }
    public float Weight { get; set; }
    public string? Comment { get; set; }
}

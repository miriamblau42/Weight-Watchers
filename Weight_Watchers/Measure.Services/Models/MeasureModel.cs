using Measure.Data.Entities;

namespace Measure.Services.Models;

public class MeasureModel
{
    public int Id { get; set; }
    public int CardId { get; set; }
    public float Weight { get; set; }
    public DateTime Date { get; set; }
    public EStatus Status { get; set; }
    public string? Comment { get; set; }
}

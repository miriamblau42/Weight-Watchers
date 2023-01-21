using Tracking.Data.Entities;

namespace Tracking.Services.Models;

public class TrackModel
{
    public int Id { get; set; }
    public int CardId { get; set; }
    public float BMI { get; set; }
    public float Weight { get; set; }
    public DateTime Date { get; set; }
    public ETrend Trend { get; set; }
    public string? Comment { get; set; }
}

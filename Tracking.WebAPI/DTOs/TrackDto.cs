using Tracking.Data.Entities;

namespace Tracking.WebAPI.DTOs
{
    public class TrackDto
    {
        //,date, weight,trend,comments, BMI
        public int CardID { get; set; }
        public DateTime date { get; set; }
        public float Weight { get; set; }
        public Trend Trend { get; set; }
        public string? Comments { get; set; }
        public float BMI { get; set; }
    }
}

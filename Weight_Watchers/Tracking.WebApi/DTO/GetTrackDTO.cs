using System.Collections.Generic;
using Tracking.Data.Entities;

namespace Tracking.WebApi.DTO
{
    public class GetTrackDTO
    {
        public float BMI { get; set; }
        public float Weight { get; set; }
        public DateTime Date { get; set; }
        public ETrend Trend { get; set; }
        public string? Comment { get; set; }
    }
}

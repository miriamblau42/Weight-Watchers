using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.Data.Entities;

namespace Tracking.Services.Models
{
    public class TackModel
    {
  
        public int CardID { get; set; }
        public DateTime Date { get; set; }
        public float Weight { get; set; }
        public Trend Trend { get; set; }
        public string? Comments { get; set; }
        public float BMI { get; set; }
    }
}

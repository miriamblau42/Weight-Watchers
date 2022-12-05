using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Data.Entities
{
    public class Tracking
    {
        //Id,CardId, weight, date, trend, BMI, comments
        [Key]
        public int ID { get; set; }
        [Required]
        [Range(0,int.MaxValue)]
      
        public int CardID { get; set; }
        [Range(0,300)]
        [Required]
        public float Weight { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public Trend Trend { get; set; }
        public float BMI { get; set; }
    }
    public enum Trend
    {
        Up,Down,Static
    }
}

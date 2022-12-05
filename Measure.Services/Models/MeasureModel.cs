using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Measure.Services.Models
{
    public class MeasureModel
    {
       
        public int ID { get; set; }
        public int CardID { get; set; } 
        public float Weight { get; set; }  
        public DateTime Date { get; set; }
        public Status Status { get; set; }
        public string? Comments { get; set; }

    }
    public enum Status
    {
        InProgress=0,Succsesed=1,Failed=2
    }
}

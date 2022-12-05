using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Messages
{
    public class TrackRowAdd :ICommand
    {
        
        public int CardID { get; set; }
        public int MeasureID { get; set; }
        public float Weight { get; set; }
        public float BMI { get; set; }
    }
}

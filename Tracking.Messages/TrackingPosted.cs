using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Messages
{
    public class TrackingPosted:IEvent
    {
        public int CardID { get; set; }
        public int MeasureID { get; set; }
        public bool Success { get; set; }
    }
}

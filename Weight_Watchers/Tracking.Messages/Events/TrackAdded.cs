using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracking.Messages.Events
{
    public class TrackAdded
    {
        public int MeasureId { get; set; }
        public int CardId { get; set; }
        public bool Succeeded { get; set; }
    }
}

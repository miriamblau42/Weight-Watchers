using NServiceBus;

namespace Measure.Messages
{
    public class MeasureInserted :IEvent
    {
        public int MesureID { get; set; }
        public int CardID { get; set; }
        public float Weight { get; set; }
    }
}

using NServiceBus;

namespace Measure.Messages.Commands
{
    public class UpdateMeasure :ICommand
    {
        public int MeasureId { get; set; }
        public int CardId { get; set; }
        public float Weight { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
    }
}

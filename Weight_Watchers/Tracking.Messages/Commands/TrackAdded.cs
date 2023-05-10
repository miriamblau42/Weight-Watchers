using NServiceBus;


namespace Tracking.Messages.Commands
{
    public class TrackAdded : ICommand
    {
        public int MeasureId { get; set; }
        public int CardId { get; set; }
        public bool Succeeded { get; set; }
    }
}

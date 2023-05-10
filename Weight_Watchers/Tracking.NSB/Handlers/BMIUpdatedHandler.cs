using NServiceBus;
using Subscriber.Messages.Commands;
using Tracking.Messages.Commands;
using Tracking.Services;
using Tracking.Services.Models;

namespace Tracking.NSB.Handlers
{
    public class BMIUpdatedHandler : IHandleMessages<BMIupdated>
    {
        private readonly ITrackingService _trackingService;

        public BMIUpdatedHandler(ITrackingService trackingService)
        {
            _trackingService = trackingService;
        }
        public async Task Handle(BMIupdated message, IMessageHandlerContext context)
        {
            TrackAdded trackAdded = new()
            {
                MeasureId = message.MeasureId,
                CardId = message.CardId,
                Succeeded = true
            };
            TrackModel trackModel = new TrackModel()
            {
                CardId = message.CardId,
                BMI = message.BMI,
                Weight = message.Weight,
                Date = message.Date,
                Comment = message.Comment,
            };
            try
            {
                await _trackingService.AddNewTrack(trackModel);
            }
            catch 
            {
                trackAdded.Succeeded = false;
            }
            var options = new SendOptions();
            options.SetDestination("Measure");
            await context.Send(trackAdded,options);
            Console.WriteLine($"Message 'BMIupdated' with Measure: {message.MeasureId} received at endpoint");
        }
    }
}

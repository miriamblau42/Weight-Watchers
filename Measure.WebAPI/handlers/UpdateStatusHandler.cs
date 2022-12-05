using Measure.Messages;
using Measure.Services.Interfaces;
using Measure.Services.Models;
using NServiceBus;
using NServiceBus.Logging;

namespace Measure.WebAPI.handlers
{
    public class UpdateStatusHandler
    {
        static ILog log = LogManager.GetLogger<UpdateStatusHandler>();
        IMeasureService measureService;
        public UpdateStatusHandler(IMeasureService measureService)
        {
            this.measureService = measureService;
        }
        public Task Handle(UpdateStatus message, IMessageHandlerContext context)
        {
            log.Info("UpdateStatus message received.");
            Status status;
            if (message.Success)
                status = Status.Succsesed;
            else
                status = Status.Failed;
            return measureService.UpdateStatus(message.MeasureID, status);
        }
    }
}

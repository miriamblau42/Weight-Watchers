using Measure.Messages.Commands;
using Measure.Messages.Events;
using Measure.Services;
using NServiceBus;
using NServiceBus.Logging;
using Tracking.Messages.Commands;

namespace Measure.NSB;

public class MeasurePolicy : Saga<MeasurePolicyData>, IAmStartedByMessages<MeasureAdded>
                               , IHandleMessages<TrackAdded>
{
    static ILog log = LogManager.GetLogger<MeasurePolicy>();
    private readonly IMeasureService _measureService;

    public MeasurePolicy(IMeasureService measureService)
    {
        _measureService = measureService;
    }

    public async Task Handle(MeasureAdded message, IMessageHandlerContext context)
    {
        log.Info($"Received MeasureAdded, Measure = {message.MeasureId}");
        Data.IsMeasureAdded = true;
        var options = new SendOptions();
        options.SetDestination("Subscriber");
        await context.Send(new UpdateMeasure()
        {
            MeasureId = message.MeasureId,
            CardId = message.CardId,
            Date = message.Date,
            Weight = message.Weight,
            Comment = message.Comment,
        }, options);
        SagaComplete(context);
    }

    public Task Handle(TrackAdded message, IMessageHandlerContext context)
    {
        log.Info($"Received TrackAdded, Measure = {message.MeasureId}");
        Data.IsTrackAdded = true;
        _measureService.UpdateMeasureStatus(Data.MeasureId, message.Succeeded);
        return SagaComplete(context);
    }

    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<MeasurePolicyData> mapper)
    {
        mapper.MapSaga(sagaData => sagaData.MeasureId)
                   .ToMessage<MeasureAdded>(message => message.MeasureId)
                   .ToMessage<TrackAdded>(message => message.MeasureId);
    }
    private async Task SagaComplete(IMessageHandlerContext context)
    {
        if (Data.IsMeasureAdded && Data.IsTrackAdded)
        {
            log.Info($"Complete Sagad");

            MarkAsComplete();
        }
    }

}


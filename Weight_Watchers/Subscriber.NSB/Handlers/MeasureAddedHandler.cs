using Measure.Messages.Events;
using NServiceBus;
using Subscriber.Data;
using Subscriber.Data.Entities;
using Subscriber.Messages.Events;

namespace Subscriber.NSB.Handlers;

public class MeasureAddedHandler : IHandleMessages<MeasureAdded>
{
    private readonly ISubscriberData _subscriberData;

    public MeasureAddedHandler(ISubscriberData subscriberData)
    {
        _subscriberData = subscriberData;
    }
    public async Task Handle(MeasureAdded message, IMessageHandlerContext context)
    {
        if (!await _subscriberData.CardExists(message.CardId)) throw new KeyNotFoundException("Card doesn't exist in database.");
        Card updatedCard = await _subscriberData.UpdateBMI(message.CardId, message.Weight);
        if (updatedCard == null) throw new Exception("Failed writing to database.");
        BMIupdated BMIUpdated = new()
        {
            MeasureId = message.MeasureId,
            Date = message.Date,
            CardId = updatedCard.Id,
            Weight = updatedCard.Weight,
            BMI = updatedCard.BMI,
            Comment = message.Comment
        };
        await context.Publish(BMIUpdated);
        Console.WriteLine($"Message 'MeasureAdded' with MeasureId: {message.MeasureId} received at endpoint");
        await Task.CompletedTask;
    }
}

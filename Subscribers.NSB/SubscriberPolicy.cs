using Measure.Messages;
using NServiceBus;


namespace Subscribers.NSB;

public class SubscriberPolicy : Saga<SubscriberPolicyData>, IAmStartedByMessages<MeasureInserted>
{
    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SubscriberPolicyData> mapper)
    {
        mapper.MapSaga(data => data.MeasureID).ToMessage<MeasureInserted>(message => message.MesureID);
    }

    public Task Handle(MeasureInserted message, IMessageHandlerContext context)
    {
        Console.WriteLine("measure recived");
        //BMI...
        return Task.CompletedTask;
    }
   


}

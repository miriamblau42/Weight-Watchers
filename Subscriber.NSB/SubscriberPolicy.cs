using Measure.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Logging;
using Subscriber.Data;
using Subscriber.Data.Entities;
using Subscriber.Data.Interfaces;
using Subscriber.Services.Interfaces;
using Subscriber.Services.Services;
using Tracking.Messages;

namespace Subscribers.NSB;

public class SubscriberPolicy : Saga<SubscriberPolicyData>, IAmStartedByMessages<MeasureInserted>, IAmStartedByMessages<TrackingPosted>
{
    static ILog log = LogManager.GetLogger<SubscriberPolicy>();
    private ServiceProvider _servicesFactory;
    public SubscriberPolicy()
    {
        var databaseConnection = "Data Source=localhost\\sqlexpress;Initial Catalog=Subscriber;Integrated Security=True";
        IServiceCollection serviceCollection = new ServiceCollection();
        serviceCollection.AddScoped<ICardService, CardService>();
        serviceCollection.AddScoped<ICardDal, SubscriberDal>();
        serviceCollection.AddDbContext<SubscribeDBContext>(opt => opt.UseSqlServer(databaseConnection));
        _servicesFactory = serviceCollection.BuildServiceProvider();
    }
    protected override void ConfigureHowToFindSaga(SagaPropertyMapper<SubscriberPolicyData> mapper)
    {
        mapper.MapSaga(data => data.MeasureID)
            .ToMessage<MeasureInserted>(message => message.MesureID)
            .ToMessage<TrackingPosted>(message=>message.MeasureID);
    }

    public async Task Handle(MeasureInserted message, IMessageHandlerContext context)
    {
        log.Info($"WeightUpdated message received.");
        Data.IsUpdateBMI = true;
        var _subscribeService = _servicesFactory.GetService<ICardService>();


        if (await _subscribeService.ExistCardId(message.CardID))
        {
            float BMI = await _subscribeService.UpdateBMIAndWeight(message.CardID, message.Weight);
            if (BMI == 0)
                CompleteSagaAsync(message.MesureID, false, context);
            else
            {
                TrackRowAdd updateTracking = new TrackRowAdd()
                {
                    MeasureID = message.MesureID,
                    CardID = message.CardID,
                    Weight = message.Weight,
                    BMI = BMI
                };
                try
                {
                    await context.Send(updateTracking);
                }
                catch
                {
                    log.Info("need to send to tracking");
                }
               
            }
        }
        else
            CompleteSagaAsync(message.MesureID, false, context);
    }
    public Task Handle(TrackingPosted message, IMessageHandlerContext context)
    {
        log.Info($"TrackingDone message received.");
        Data.IsTracked = true;
        CompleteSagaAsync(message.MeasureID, message.Success, context);
        return Task.CompletedTask;
    }



    private async Task CompleteSagaAsync(int measureID, bool v, IMessageHandlerContext context)
    {
        UpdateStatus updateStatus = new UpdateStatus()
                                    { MeasureID = measureID, Success = v };
        await context.Send(updateStatus);
        log.Info("Saga Completed!");
        MarkAsComplete();
    }

}

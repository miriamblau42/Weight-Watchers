
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Logging;
using System.Data.SqlClient;
using Tracking.Data;
using Tracking.Data.Entities;
using Tracking.Data.Interfaces;
using Tracking.Services.Interfaces;
using Tracking.Services.Services;

class Program
{
    static async Task Main()
    {
        Console.Title = "Tracking";

        var endpointConfiguration = new EndpointConfiguration("Tracking");
        var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
        containerSettings.ServiceCollection.AddScoped<ITrackService,TrackService >();
        containerSettings.ServiceCollection.AddScoped<ITrackDal, TrackDal>();
        containerSettings.ServiceCollection.AddDbContext<TrackContext>(options => options.UseSqlServer("Data Source = localhost\\sqlexpress; Initial Catalog = Tracking; Integrated Security = True;"));
        containerSettings.ServiceCollection.AddAutoMapper(typeof(Program));


        /*logFetal*/
        var defaultFactory = LogManager.Use<DefaultFactory>();
        defaultFactory.Level(LogLevel.Fatal);

    /*    recoverability*/
       var recoverability = endpointConfiguration.Recoverability();
        recoverability.Immediate(
            customizations: immediate =>
            {
                immediate.NumberOfRetries(3);
            });
        recoverability.Delayed(
            customizations: delayed =>
            {
                delayed.NumberOfRetries(4);
                delayed.TimeIncrease(TimeSpan.FromSeconds(1));
            });

        endpointConfiguration.EnableInstallers();
       /* outbox*/
        endpointConfiguration.EnableOutbox();
    /*    Persistance*/
      var connection = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Tracking;Integrated Security=True";
        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        var subscriptions = persistence.SubscriptionSettings();
        subscriptions.CacheFor(TimeSpan.FromMinutes(1));
        var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
        dialect.Schema("dbo");
        dialect.DoNotUseSqlServerTransportConnection();
        persistence.ConnectionBuilder(
            connectionBuilder: () =>
            {
                return new SqlConnection(connection);
            });

  
        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.UseConventionalRoutingTopology(QueueType.Quorum);
        transport.ConnectionString("host=localhost");

        var endpointInstance = await Endpoint.Start(endpointConfiguration);

        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();

        await endpointInstance.Stop();
    }
}

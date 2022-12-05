using Measure.Messages;
using Microsoft.Data.SqlClient;
using NServiceBus;
using NServiceBus.Logging;
using Tracking.Messages;

class Program
{
    static async Task Main()
    {
        Console.Title = "Subscriber";
        var endpointConfiguration = new EndpointConfiguration("Subscriber");

        var defaultFactory = LogManager.Use<DefaultFactory>();
        /*       defaultFactory.Level(LogLevel.Fatal);*/
        /* recoverability*/
        // services container
       // var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());

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
        /*outbox*/
        endpointConfiguration.EnableOutbox();
        /*  Persistance*/
        var connection = "Data Source=localhost\\sqlexpress;Initial Catalog=Subscriber;Integrated Security=True";

        //var connection = @"server=DESKTOP-411ES1J\\ADMIN;Database=Subscriber;Trusted_Connection=True";
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
        /*RabbitMQTransport*/
        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.UseConventionalRoutingTopology(QueueType.Quorum);
        transport.ConnectionString("host=localhost");

        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(TrackRowAdd), destination: "Tracking");
        routing.RouteToEndpoint(typeof(UpdateStatus), destination: "Measure.NSB");

        var endpointInstance = await Endpoint.Start(endpointConfiguration);
        Console.WriteLine("Press Enter to exit.");
        Console.ReadLine();
        await endpointInstance.Stop();
    }
}








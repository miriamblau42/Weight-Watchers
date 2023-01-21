
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Logging;
using Tracking.Data;
using Tracking.Data.Entities;
using Tracking.Services;

class Program
{
    static ILog log = LogManager.GetLogger<Program>();

    static async Task Main()
    {
        Console.Title = "Tracking";

        var endpointConfiguration = new EndpointConfiguration("Tracking");

        var databaseConnection = "Data Source=localhost\\sqlexpress; Initial Catalog=Tracking; Integrated Security=True";
        var rabbitMQConnection = @"host=localhost";

        var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
        containerSettings.ServiceCollection.AddScoped<ITrackingService, TrackingService>();
        containerSettings.ServiceCollection.AddScoped<ITrackingData, TrackingData>();
        containerSettings.ServiceCollection.AddAutoMapper(typeof(Program));
        containerSettings.ServiceCollection.AddDbContextFactory<TrackingContext>(opt => opt.UseSqlServer(databaseConnection));

        #region ReceiverConfiguration

        endpointConfiguration.EnableInstallers();
        endpointConfiguration.EnableOutbox();

        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString(rabbitMQConnection);
        transport.UseConventionalRoutingTopology(QueueType.Quorum);

        var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        persistence.ConnectionBuilder(
            connectionBuilder: () =>
            {
                return new SqlConnection(databaseConnection);
            });

        var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
        dialect.Schema("dbo");

        var conventions = endpointConfiguration.Conventions();
        conventions.DefiningEventsAs(type => type.Namespace == "Tracking.Messages.Events");
        #endregion 

        var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

        Console.WriteLine("waiting for messages...");
        Console.ReadLine();

        await endpointInstance.Stop();
    }
}

using Measure.Data;
using Measure.Data.Entities;
using Measure.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;
using NServiceBus.Logging;


class Program
{
    static ILog log = LogManager.GetLogger<Program>();

    static async Task Main()
    {
        Console.Title = "Measure";

        var endpointConfiguration = new EndpointConfiguration("Measure");

        var databaseConnection = "Data Source=localhost\\sqlexpress; Initial Catalog=Subscriber; Integrated Security=True";        var rabbitMQConnection = @"host=localhost";

        var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());
        containerSettings.ServiceCollection.AddScoped<IMeasureService, MeasureService>();
        containerSettings.ServiceCollection.AddScoped<IMeasureData, MeasureData>();
        containerSettings.ServiceCollection.AddAutoMapper(typeof(Program));
        containerSettings.ServiceCollection.AddDbContextFactory<MeasureContext>(opt => opt.UseSqlServer(databaseConnection));

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
        conventions.DefiningEventsAs(type => type.Namespace == "Measure.Messages.Events");
        #endregion 

        var endpointInstance = await Endpoint.Start(endpointConfiguration).ConfigureAwait(false);

        Console.WriteLine("waiting for messages...");
        Console.ReadLine();

        await endpointInstance.Stop();
    }
}
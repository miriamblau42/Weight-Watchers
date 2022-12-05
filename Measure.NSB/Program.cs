using Measure.Data;
using Measure.Data.Entities;
using Measure.Data.Interfaces;
using Measure.Services.Interfaces;
using Measure.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus.Logging;
using System.Data.SqlClient;

public class Program
{
    static ILog log = LogManager.GetLogger<Program>();

    static async Task Main()
    {
        Console.Title = "Measure.NSB";

        var endpointConfiguration = new EndpointConfiguration("Measure.NSB");

        var databaseConnection = "Data Source=localhost\\sqlexpress;Initial Catalog=Measure;Integrated Security=True";
        var rabbitMQConnection = @"host=localhost";
  /*     
        var servicesFactory = new DefaultServiceProviderFactory();
        servicesFactory.CreateServiceProvider(serviceCollection);
        endpointConfiguration.UseContainer(servicesFactory);*/
        // var containerSettings = endpointConfiguration.UseContainer(new DefaultServiceProviderFactory());

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
        var subscriptions = persistence.SubscriptionSettings();
        subscriptions.CacheFor(TimeSpan.FromMinutes(1));
        dialect.Schema("dbo");
        #endregion

        var conventions = endpointConfiguration.Conventions();
        conventions.DefiningCommandsAs(type => type.Namespace == "Messages.Commands");

        var endpointInstance = await Endpoint.Start(endpointConfiguration);

        Console.WriteLine("waiting for messages...");
        Console.ReadLine();

        await endpointInstance.Stop();
    }
}

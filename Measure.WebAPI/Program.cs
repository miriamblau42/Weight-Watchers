using Measure.Data;
using Measure.Data.Entities;
using Measure.Data.Interfaces;
using Measure.Services.Interfaces;
using Measure.Services.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NServiceBus;

//endpointNsb

var builder = WebApplication.CreateBuilder(args);

#region back-end-use-nservicebus
builder.Host.UseNServiceBus(hostBuilderContext =>
    {
        var endpointConfiguration = new EndpointConfiguration("Measure");
        /*endpointConfiguration.EnableInstallers();
        endpointConfiguration.EnableOutbox();*/
        endpointConfiguration.SendOnly();
        /*var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
        var connection = @"Data Source=DESKTOP-411ES1J\ADMIN;Initial Catalog=Measure;Integrated Security=True";
        persistence.ConnectionBuilder(
        connectionBuilder: () =>
        {
            return new SqlConnection(connection);
        });
        var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
        dialect.Schema("measurPresistence");*/
        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        var rabbitMQConnection = @"host=localhost";
        transport.ConnectionString(rabbitMQConnection);
        transport.UseConventionalRoutingTopology(QueueType.Quorum);
        return endpointConfiguration;
    });
#endregion
IConfigurationRoot configuration = new
            ConfigurationBuilder().AddJsonFile("appsettings.json",
            optional: false, reloadOnChange: true).Build();
// Add services to the container.
builder.Services.AddDbContextFactory<MeasureDBContext>(options => options.UseSqlServer(
configuration.GetConnectionString("ConnectMeasurerDb")));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IMeasureDal, MeasureDal>();
builder.Services.AddScoped<IMeasureService, MeasureService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(options => {
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();

});
//app.UseHandlerErrorsMiddleware();
app.UseAuthorization();
app.UseCors("AllowAll");

app.MapControllers();

app.Run();
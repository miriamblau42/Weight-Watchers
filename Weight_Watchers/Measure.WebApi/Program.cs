using Measure.Data;
using Measure.Data.Entities;
using Measure.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NServiceBus;

using EndpointConfiguration = NServiceBus.EndpointConfiguration;

var builder = WebApplication.CreateBuilder(args);

var databaseConnection = "Data Source=localhost\\sqlexpress; Initial Catalog=Subscriber; Integrated Security=True";
var rabbitMQConnection = "host=localhost";

#region back-end-use-nservicebus
builder.Host.UseNServiceBus(hostBuilderContext =>
{
    var endpointConfiguration = new EndpointConfiguration("MeasureApi");
    endpointConfiguration.EnableInstallers();
    endpointConfiguration.EnableOutbox();
    endpointConfiguration.SendOnly();

    var persistence = endpointConfiguration.UsePersistence<SqlPersistence>();
    persistence.ConnectionBuilder(
    connectionBuilder: () =>
    {
        return new SqlConnection(databaseConnection);
    });
    var dialect = persistence.SqlDialect<SqlDialect.MsSqlServer>();
    dialect.Schema("dbo");

    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.ConnectionString(rabbitMQConnection);
    transport.UseConventionalRoutingTopology(QueueType.Quorum);

    return endpointConfiguration;
});

#endregion


// Add services to the container.
builder.Services.AddScoped<IMeasureData, MeasureData>();
builder.Services.AddScoped<IMeasureService, MeasureService>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContextFactory<MeasureContext>(opt => opt.UseSqlServer(databaseConnection));

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", p =>
    {
        p.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

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

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();


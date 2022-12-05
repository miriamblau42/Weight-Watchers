using Microsoft.EntityFrameworkCore;
using Subscriber.Data;
using Subscriber.Data.Entities;
using Subscriber.Data.Interfaces;
using Subscriber.Services.Interfaces;
using Subscriber.Services.Services;

//endpointNsb

var builder = WebApplication.CreateBuilder(args);


IConfigurationRoot configuration = new
            ConfigurationBuilder().AddJsonFile("appsettings.json",
            optional: false, reloadOnChange: true).Build();
// Add services to the container.
builder.Services.AddDbContext<SubscribeDBContext>(options => options.UseSqlServer(
configuration.GetConnectionString("ConnectSubscriberDb")));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ISubscriberDal, SubscriberDal>();
builder.Services.AddScoped<ICardDal, SubscriberDal>();
builder.Services.AddScoped<ISubscriberService, SubscriberService>();
builder.Services.AddScoped<ICardService, CardService>();


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

app.MapControllers();

app.Run();










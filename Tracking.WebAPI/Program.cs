using Microsoft.EntityFrameworkCore;
using Tracking.Data;
using Tracking.Data.Entities;
using Tracking.Data.Interfaces;
using Tracking.Services.Interfaces;
using Tracking.Services.Services;

var builder = WebApplication.CreateBuilder(args);
IConfigurationRoot configuration = new
            ConfigurationBuilder().AddJsonFile("appsettings.json",
            optional: false, reloadOnChange: true).Build();
// Add services to the container.
builder.Services.AddDbContext<TrackContext>(options => options.UseSqlServer(
configuration.GetConnectionString("ConnectTrackDb")));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ITrackDal, TrackDal>();
builder.Services.AddScoped<ITrackService, TrackService>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();

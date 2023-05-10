using Microsoft.EntityFrameworkCore;
using Tracking.Data;
using Tracking.Data.Entities;
using Tracking.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ITrackingData, TrackingData>();
builder.Services.AddScoped<ITrackingService, TrackingService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContextFactory<TrackingContext>(opt => opt.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=Tracking; Integrated Security=True"));


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

using CoronaApp.Api.Middlewares;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Subscriber.Data;
using Subscriber.Data.Entities;
using Subscriber.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<ISubscriberData, SubscriberData>();
builder.Services.AddScoped<ISubscriberService, SubscriberService>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddDbContextFactory<SubscriberContext>(opt => opt.UseSqlServer("Data Source=localhost\\sqlexpress; Initial Catalog=Subscriber; Integrated Security=True"));

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
builder.Services.AddSwaggerGen(c=> 
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weight Watchers", Version = "v1" })
    );
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weight Watchers V1");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseHandleErrorMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();

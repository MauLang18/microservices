using Carter;
using HealthChecks.UI.Client;
using Microservice.Product.Api.Middleware;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Taller.Microservices.EventBus;
using Taller.Microservices.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<RabbitMQSettings>(x => builder.Configuration.GetSection("RabbitMQSettings"));
builder.Services.AddServicesInjection(builder.Configuration);
builder.Services.AddHealthCheck(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.AddMiddlewareValidation();

app.UseCors();

app.MapControllers();
app.MapCarter();
app.MapHealthChecksUI();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

public partial class Program { }
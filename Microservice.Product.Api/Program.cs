using HealthChecks.UI.Client;
using Microservice.Product.Api.Middleware;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Taller.Microservices.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

DependencyInjection.AddServicesInjection(builder.Services, builder.Configuration);
builder.Services.AddHealthCheck(builder.Configuration);

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

app.AddMiddlewareValidation();

app.MapControllers();

app.MapHealthChecksUI();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();

public partial class Program { }
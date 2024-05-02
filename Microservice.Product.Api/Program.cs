using Microservice.Product.Api.Middleware;
using Taller.Microservices.IoC;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

DependencyInjection.AddServicesInjection(builder.Services, builder.Configuration);

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

app.Run();

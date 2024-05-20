using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Taller.Microservices.EventBus;
using Microservice.Product.Application;
using Microservice.Product.Infrastructure;
using MediatR;
using Microservice.Product.Application.UseCases.Product.Commands.CreateEventCommand;

namespace Taller.Microservices.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddServicesInjection(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddTransient<IRequestHandler<CreateOrderProductCommand, bool>, CreateOrderProductHandler>();
        services.AddEventBus();
        services.AddApplication();
        services.AddInfrastructure(configuration);

        return services;
    }
}
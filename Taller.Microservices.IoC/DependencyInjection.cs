using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Taller.Microservices.EventBus;
using Microservice.Product.Application;
using Microservice.Product.Infrastructure;

namespace Taller.Microservices.IoC;

public static class DependencyInjection
{
    public static void AddServicesInjection(IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddEventBus(configuration);
        services.AddApplication();
        services.AddInfrastructure(configuration);
    }
}
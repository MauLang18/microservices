using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Taller.Microservices.Domain.Bus;
using Taller.Microservices.EventBus;

namespace Taller.Microservices.IoC;

public static class DependencyInjection
{
    public static void AddServicesInjection(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IEventBus, RabbitMQEventBus>();
        services.Configure<RabbitMQSettings>(x => configuration.GetSection("RabbitMQSettings"));
    }
}
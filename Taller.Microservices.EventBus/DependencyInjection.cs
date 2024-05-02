using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Taller.Microservices.Domain.Bus;

namespace Taller.Microservices.EventBus;

public static class DependencyInjection
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IEventBus, RabbitMQEventBus>();
        services.Configure<RabbitMQSettings>(x => configuration.GetSection("RabbitMQSettings"));

        return services;
    }
}
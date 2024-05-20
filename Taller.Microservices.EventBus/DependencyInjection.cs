using Microsoft.Extensions.DependencyInjection;
using Taller.Microservices.Domain.Bus;

namespace Taller.Microservices.EventBus;

public static class DependencyInjection
{
    public static IServiceCollection AddEventBus(this IServiceCollection services)
    {
        services.AddTransient<IEventBus, RabbitMQEventBus>();

        return services;
    }
}
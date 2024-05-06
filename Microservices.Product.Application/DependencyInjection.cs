using FluentValidation;
using MediatR;
using Microservice.Product.Application.Commons.Behaviors;
using Microservice.Product.Application.Interfaces.Events;
using Microservice.Product.Application.UseCases.Product.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Microservice.Product.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(x => x.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<IProductEventBus, ProductEventBus>();

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviors<,>));

        return services;
    }
}
using Microservice.Product.Infrastructure.Persistence.Context;
using Microservice.Product.Infrastructure.Persistence.Repositories;
using Microservice.Product.Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microservice.Product.Application.Interfaces.Services;
using Microservice.Product.Infrastructure.Services;

namespace Microservice.Product.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        var assembly = typeof(ApplicationDbContext).Assembly.FullName;

        services.AddDbContext<ApplicationDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("MicroserviceProductConnection"), b => b.MigrationsAssembly(assembly)));

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
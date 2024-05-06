namespace Microservice.Product.Api.Middleware;

public static class HealthCheckExtension
{
    public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddNpgSql(configuration.GetConnectionString("MicroserviceProductConnection")!, tags: new[] { "database" });

        services
            .AddHealthChecksUI()
            .AddInMemoryStorage();

        return services;
    }
}
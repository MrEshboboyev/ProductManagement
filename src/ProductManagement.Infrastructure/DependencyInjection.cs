using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Domain.Repositories;
using ProductManagement.Infrastructure.Persistence;
using ProductManagement.Infrastructure.Persistence.Repositories;

namespace ProductManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register MongoDB
        services.Configure<MongoDbSettings>(
            configuration.GetSection("MongoDB").Bind); // Fix the error by using .Bind

        services.AddSingleton<MongoDbContext>();

        // Register repositories
        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}

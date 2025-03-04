using FluentValidation;
using MediatR;
using ProductManagement.Application.Common.Behaviors;
using ProductManagement.Application.Common.Mappings;
using ProductManagement.Application.Products.Commands.CreateProduct;
using ProductManagement.Infrastructure;

namespace ProductManagement.Api.Extensions;

public static class WebApplicationExtensions
{
    public static IServiceCollection AddDefaultServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Add application layer
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly));
        services.AddAutoMapper(typeof(MappingProfile).Assembly);
        services.AddValidatorsFromAssembly(typeof(CreateProductCommand).Assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // Add infrastructure layer
        services.AddInfrastructure(configuration);

        return services;
    }
}

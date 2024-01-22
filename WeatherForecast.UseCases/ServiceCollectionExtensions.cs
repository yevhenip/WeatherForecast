using FluentValidation;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Core.Behaviours;

namespace WeatherForecast.UseCases;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        return services
            .AddMediator()
            .AddMapper();
    }

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(AssemblyMarker.Assembly);
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssembly(AssemblyMarker.Assembly);

        return services;
    }

    private static IServiceCollection AddMapper(this IServiceCollection services)
    {
        services.AddSingleton<TypeAdapterConfig>(_ =>
        {
            var typeAdapterConfig = new TypeAdapterConfig();

            typeAdapterConfig.Scan(AssemblyMarker.Assembly);

            return typeAdapterConfig;
        });
        services.AddSingleton<IMapper, ServiceMapper>();

        return services;
    }
}
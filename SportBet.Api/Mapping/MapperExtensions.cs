
using Mapster;
using MapsterMapper;
using System.Reflection;

namespace SportBet.Api.Mapping;
public static class MapperExtensions
{
    public static IServiceCollection AddMapping(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);

        services.AddScoped<IMapper, ServiceMapper>();

        return services;
    }
}
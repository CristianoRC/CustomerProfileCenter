using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace CustomerProfileCenter.Infra.Data;

public static class Setup
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration config)
    {
        ConfigureRedis(services, config);
        return services;
    }

    private static void ConfigureRedis(IServiceCollection services, IConfiguration config)
    {
        var multiplexer = ConnectionMultiplexer.Connect(config["RedisConnectionString"]);
        services.AddSingleton<IConnectionMultiplexer>(multiplexer);
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerProfileCenter.Infra.Data;

public static class Setup
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration config,
        bool isDevelopment)
    {
        ConfigureDistributedCache(services, config, isDevelopment);
        return services;
    }

    private static void ConfigureDistributedCache(IServiceCollection services, IConfiguration config, bool isDevelopment)
    {
        if (isDevelopment)
            services.AddMemoryCache();
        else
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = config["RedisConnectionString"];
            });
    }
}
using CustomerProfileCenter.Domain.Repositories;
using CustomerProfileCenter.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace CustomerProfileCenter.Infra.Data;

public static class Setup
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration config,
        bool isDevelopment)
    {
        ConfigureDistributedCache(services, config, isDevelopment);
        ConfigureMongoDb(services, config);

        var databaseName = config["DatabaseName"];
        services.AddTransient<ICustomerRepository>(provider =>
        {
            var databaseConnection = provider.GetService<IMongoClient>().GetDatabase(databaseName);
            return new CustomerRepository(databaseConnection);
        });
        return services;
    }

    private static void ConfigureMongoDb(IServiceCollection services, IConfiguration configuration)
    {
        var client = new MongoClient(configuration["MongoDbConnectionString"]);
        services.AddSingleton<IMongoClient>(client);
    }

    private static void ConfigureDistributedCache(IServiceCollection services, IConfiguration config,
        bool isDevelopment)
    {
        /*if (isDevelopment)
            services.AddMemoryCache();
        else*/
        services.AddStackExchangeRedisCache(options => { options.Configuration = config["RedisConnectionString"]; });
    }
}
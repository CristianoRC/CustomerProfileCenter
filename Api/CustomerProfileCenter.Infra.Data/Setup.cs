using CustomerProfileCenter.Domain.Repositories;
using CustomerProfileCenter.Infra.Data.HashAndCryptography;
using CustomerProfileCenter.Infra.Data.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace CustomerProfileCenter.Infra.Data;

public static class Setup
{
    public static IServiceCollection AddData(this IServiceCollection services, IConfiguration config,
        bool isDevelopment)
    {
        ConfigureDistributedCache(services, config, isDevelopment);
        ConfigureMongoDb(services, config);
        services.AddScoped<IDocumentSecurityService, DocumentSecurityService>();

        services.AddScoped<ICustomerRepository>(provider =>
        {
            var databaseName = config["DatabaseName"];
            var databaseConnection = provider.GetService<IMongoClient>().GetDatabase(databaseName);
            var documentSecurityService = provider.GetService<IDocumentSecurityService>();
            return new CustomerRepository(databaseConnection, documentSecurityService!);
        });

        return services;
    }

    private static void ConfigureMongoDb(IServiceCollection services, IConfiguration configuration)
    {
        var client = new MongoClient(configuration["MongoDbConnectionString"]);
        services.AddSingleton<IMongoClient>(client);
        var objectDiscriminatorConvention = BsonSerializer.LookupDiscriminatorConvention(typeof(object));
        var objectSerializer = new ObjectSerializer(objectDiscriminatorConvention, GuidRepresentation.Standard);
        BsonSerializer.RegisterSerializer(objectSerializer);
    }

    private static void ConfigureDistributedCache(IServiceCollection services, IConfiguration config,
        bool isDevelopment)
    {
        if (isDevelopment)
        {
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();
        }
        else
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = config["RedisConnectionString"];
            });
    }
}
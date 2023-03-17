using CustomerProfileCenter.Application.MessageBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace CustomerProfileCenter.Infra.MessageBus.Configuration;

public static class Setup
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqConnection = ConfigureConnection(configuration);
        services.AddSingleton(rabbitMqConnection);
        services.AddTransient<ISendMessageService, SendMessageService>();
        services.AddTransient<ICustomerMessageBus, CustomerMessageBus>();
        return services;
    }

    private static IConnection ConfigureConnection(IConfiguration configuration)
    {
        var connectionString = configuration["RabbitMQConnectionString"];
        var connectionFactory = new ConnectionRabbitMq(connectionString);
        var connection = connectionFactory.GetConnection();
        return connection;
    }
}
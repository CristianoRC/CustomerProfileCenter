using RabbitMQ.Client;

namespace CustomerProfileCenter.Infra.MessageBus.Configuration;

public class ConnectionRabbitMq
{
    private readonly string _connectionString;

    public ConnectionRabbitMq(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IConnection GetConnection()
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(_connectionString),
            ClientProvidedName = "CustomerProfile",
            DispatchConsumersAsync = true
        };
        return factory.CreateConnection();
    }
}
using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace CustomerProfileCenter.Infra.MessageBus;

public class SendMessageService : ISendMessageService
{
    private readonly IConnection _rabbitMqConnection;

    public SendMessageService(IConnection rabbitMqConnection)
    {
        _rabbitMqConnection = rabbitMqConnection;
    }

    private void SendMessage(string message, string exchange, string routingKey)
    {
        var body = Encoding.UTF8.GetBytes(message);

        using var channel = _rabbitMqConnection.CreateModel();
        channel.BasicPublish(exchange, routingKey, null, body);
    }

    public void SendMessage(object message, string exchange, string routingKey)
    {
        var jsonMessage = JsonSerializer.Serialize(message);
        SendMessage(jsonMessage, exchange, routingKey);
    }
}
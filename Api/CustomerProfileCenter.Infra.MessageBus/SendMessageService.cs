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


    public void SendMessage(object message, string exchange, string routingKey)
    {
        var jsonMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonMessage);
        using var channel = _rabbitMqConnection.CreateModel();
        channel.BasicPublish(exchange, routingKey, null, body);
    }

    public void SendMessage(object message, string queue)
    {
        var jsonMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonMessage);
        using var channel = _rabbitMqConnection.CreateModel();

        channel.BasicPublish(string.Empty, queue, null, body);
    }
}
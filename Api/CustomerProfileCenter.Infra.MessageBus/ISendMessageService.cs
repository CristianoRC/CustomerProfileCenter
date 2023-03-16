namespace CustomerProfileCenter.Infra.MessageBus;

public interface ISendMessageService
{
    public void SendMessage(object message, string exchange, string routingKey);
}
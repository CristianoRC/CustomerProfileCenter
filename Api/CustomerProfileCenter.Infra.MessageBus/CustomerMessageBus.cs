using CustomerProfileCenter.Application.Customer;
using CustomerProfileCenter.Application.MessageBus;
using CustomerProfileCenter.Infra.MessageBus.Configuration;

namespace CustomerProfileCenter.Infra.MessageBus;

public class CustomerMessageBus : ICustomerMessageBus
{
    private readonly ISendMessageService _sendMessageService;

    public CustomerMessageBus(ISendMessageService sendMessageService)
    {
        _sendMessageService = sendMessageService;
    }

    public void EnqueueCreateCustomerCommand(CreateCustomerCommand command)
    {
        _sendMessageService.SendMessage(command, "CreateCustomer", string.Empty);
    }
}
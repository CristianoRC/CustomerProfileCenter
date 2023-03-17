using CustomerProfileCenter.Application.Customer;

namespace CustomerProfileCenter.Application.MessageBus;

public interface ICustomerMessageBus
{
    void EnqueueCreateCustomerCommand(CreateCustomerCommand command);
}
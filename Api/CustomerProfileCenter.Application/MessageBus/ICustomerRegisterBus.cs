using CustomerProfileCenter.Application.Customer;

namespace CustomerProfileCenter.Application.MessageBus;

public interface ICustomerRegisterBus
{
    Task EnqueueCreateCustomerCommand(CreateCustomerCommand command);
}
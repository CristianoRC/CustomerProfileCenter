using CustomerProfileCenter.Application.Response;

namespace CustomerProfileCenter.Application.Customer;

public interface ICustomerService
{
    public Task<ResponseError> EnqueueCreateCustomerCommand(CreateCustomerCommand command);

    public Task<ResponseError> CreateCustomer(CreateCustomerCommand command);
}
using CustomerProfileCenter.Application.Response;

namespace CustomerProfileCenter.Application.Customer;

public interface ICustomerService
{
    public Task<ResponseError> CreateCustomer(CreateCustomerCommand command);
}
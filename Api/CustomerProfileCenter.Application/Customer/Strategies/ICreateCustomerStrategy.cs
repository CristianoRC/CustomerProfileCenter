using CustomerProfileCenter.Application.Response;

namespace CustomerProfileCenter.Application.Customer.Strategies;

public interface ICreateCustomerStrategy
{
    EDocumentType CustomerDocumentType { get; }
    Task<ResponseError> CreateCustomer(CreateCustomerCommand command);
}
using CustomerProfileCenter.Application.Response;

namespace CustomerProfileCenter.Application.Customer.Strategies;

public class CreateCompanyStrategy : ICreateCustomerStrategy
{
    public EDocumentType CustomerDocumentType => EDocumentType.Cnpj;

    public Task<ResponseError> CreateCustomer(CreateCustomerCommand command)
    {
        throw new NotImplementedException();
    }
}
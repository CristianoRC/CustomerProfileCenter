using CustomerProfileCenter.Application.Response;

namespace CustomerProfileCenter.Application.Customer.Strategies;

public class CreateIndividualStrategy : ICreateCustomerStrategy
{
    public EDocumentType CustomerDocumentType => EDocumentType.Cpf;

    public Task<ResponseError> CreateCustomer(CreateCustomerCommand command)
    {
        throw new NotImplementedException();
    }
}
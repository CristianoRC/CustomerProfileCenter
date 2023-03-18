using CustomerProfileCenter.Application.Response;

namespace CustomerProfileCenter.Application.Customer.Strategies;

public class CreateIndividualStrategy : ICreateCustomerStrategy
{
    public EDocumentType CustomerDocumentType => EDocumentType.Cpf;

    public async Task<ResponseError> CreateCustomer(CreateCustomerCommand command)
    {
        //TODO: implementar
        throw new NotImplementedException();
    }
}
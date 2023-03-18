using CustomerProfileCenter.Application.Address;
using CustomerProfileCenter.Application.Response;
using CustomerProfileCenter.Domain.Repositories;

namespace CustomerProfileCenter.Application.Customer.Strategies;

public class CreateCompanyStrategy : ICreateCustomerStrategy
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IAddressService _addressService;

    public CreateCompanyStrategy(ICustomerRepository customerRepository, IAddressService addressService)
    {
        _customerRepository = customerRepository;
        _addressService = addressService;
    }

    public EDocumentType CustomerDocumentType => EDocumentType.Cnpj;

    public async Task<ResponseError> CreateCustomer(CreateCustomerCommand command)
    {
        throw new NotImplementedException();
    }
}
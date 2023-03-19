using CustomerProfileCenter.Application.Address;
using CustomerProfileCenter.Application.Response;
using CustomerProfileCenter.Domain.Entities;
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
        var company = new Company(command.Name, command.CorporateName, command.GetDocument());

        if (company.Document.IsValid is false)
            return new ResponseError("CNPJ Inválido.");

        if (company.NameIsValid is false)
            return new ResponseError("Nome Obrigatório.");

        company.AddEmailAddress(command.EmailAddress);
        company.AddPhoneNumber(command.PhoneNumber);

        if (command.Address?.Cep is not null)
            company.AddAddress(await GetAddress(command));

        await _customerRepository.CreateCompany(company, command);
        return new ResponseError();
    }

    private async Task<Domain.ValueObjects.Address> GetAddress(CreateCustomerCommand command)
    {
        var addressResponse = await _addressService.GetAddress(command.Address.Cep);
        if (addressResponse.Error.HasError)
            return null;
        return addressResponse.Content with {Number = command.Address.Number, Complement = command.Address.Complement};
    }
}
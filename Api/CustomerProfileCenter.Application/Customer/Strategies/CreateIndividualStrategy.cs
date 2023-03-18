using CustomerProfileCenter.Application.Address;
using CustomerProfileCenter.Application.Response;
using CustomerProfileCenter.Domain.Entities;
using CustomerProfileCenter.Domain.Repositories;

namespace CustomerProfileCenter.Application.Customer.Strategies;

public class CreateIndividualStrategy : ICreateCustomerStrategy
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IAddressService _addressService;

    public CreateIndividualStrategy(ICustomerRepository customerRepository, IAddressService addressService)
    {
        _customerRepository = customerRepository;
        _addressService = addressService;
    }

    public EDocumentType CustomerDocumentType => EDocumentType.Cpf;

    public async Task<ResponseError> CreateCustomer(CreateCustomerCommand command)
    {
        var birthday = command.Birthday is null ? (DateOnly?) null : DateOnly.FromDateTime(command.Birthday.Value);
        var individual = new Individual(command.Name, command.GetDocument(), birthday);

        if (individual.Document.IsValid is false)
            return new ResponseError("CPF Inválido.");

        if (individual.NameIsValid is false)
            return new ResponseError("Nome Obrigatório.");

        individual.AddEmailAddress(command.EmailAddress);
        individual.AddPhoneNumber(command.PhoneNumber);

        if (command.Address?.Cep is not null)
            individual.AddAddress(await GetAddress(command));

        await _customerRepository.CreateIndividual(individual, command);
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
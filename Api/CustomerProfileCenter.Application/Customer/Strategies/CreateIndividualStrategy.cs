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

        //TODO: Preencher o endereço
        //TODO: Preencer email
        //TODO: Prencher Telefone!

        await _customerRepository.CreateIndividual(individual, command);
        return new ResponseError();
    }
}
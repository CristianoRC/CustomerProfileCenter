using CustomerProfileCenter.Application.Customer.Strategies;
using CustomerProfileCenter.Application.MessageBus;
using CustomerProfileCenter.Application.Response;
using CustomerProfileCenter.Domain.Repositories;

namespace CustomerProfileCenter.Application.Customer;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IEnumerable<ICreateCustomerStrategy> _createCustomerStrategies;
    private readonly ICustomerRegisterBus _customerRegisterBus;

    public CustomerService(ICustomerRepository customerRepository,
        IEnumerable<ICreateCustomerStrategy> createCustomerStrategies,
        ICustomerRegisterBus customerRegisterBus)
    {
        _customerRepository = customerRepository;
        _createCustomerStrategies = createCustomerStrategies;
        _customerRegisterBus = customerRegisterBus;
    }

    public async Task<ResponseError> EnqueueCreateCustomerCommand(CreateCustomerCommand command)
    {
        var userAlreadyRegistered = await _customerRepository.CustomerAlreadyRegistered(command.GetDocument());
        if (userAlreadyRegistered)
            return new ResponseError("Cliente já cadastrado");

        //TODO: Validar outros campos?
        await _customerRegisterBus.EnqueueCreateCustomerCommand(command);
        return new ResponseError();
    }

    public async Task<ResponseError> CreateCustomer(CreateCustomerCommand command)
    {
        var userAlreadyRegistered = await _customerRepository.CustomerAlreadyRegistered(command.GetDocument());
        if (userAlreadyRegistered)
            return new ResponseError("Cliente já cadastrado");

        var strategy = _createCustomerStrategies.FirstOrDefault(x => x.CustomerDocumentType == command.DocumentType);
        if (strategy is null)
            throw new ArgumentOutOfRangeException(nameof(command.DocumentType),
                "Strategy não encontrada para este tipo de document");

        return await strategy.CreateCustomer(command);
    }
}
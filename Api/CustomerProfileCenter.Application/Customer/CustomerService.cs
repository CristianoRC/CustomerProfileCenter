using CustomerProfileCenter.Application.Customer.Strategies;
using CustomerProfileCenter.Application.MessageBus;
using CustomerProfileCenter.Application.Response;
using CustomerProfileCenter.Domain.Repositories;

namespace CustomerProfileCenter.Application.Customer;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IEnumerable<ICreateCustomerStrategy> _createCustomerStrategies;
    private readonly ICustomerMessageBus _customerMessageBus;

    public CustomerService(ICustomerRepository customerRepository,
        IEnumerable<ICreateCustomerStrategy> createCustomerStrategies,
        ICustomerMessageBus customerMessageBus)
    {
        _customerRepository = customerRepository;
        _createCustomerStrategies = createCustomerStrategies;
        _customerMessageBus = customerMessageBus;
    }

    public async Task<ResponseError> EnqueueCreateCustomerCommand(CreateCustomerCommand command)
    {
        var (commandHasError, commandErrors) = command.GetValidationErrors();
        if (commandHasError)
            return new ResponseError(commandErrors);

        if (command.DocumentIsValid() is false)
            return new ResponseError("Documento Inválido");

        var userAlreadyRegistered = await _customerRepository.CustomerAlreadyRegistered(command.GetDocument());
        if (userAlreadyRegistered)
            return new ResponseError("Cliente já cadastrado");

        command.SetIdempotencyKey();
        _customerMessageBus.EnqueueCreateCustomerCommand(command);
        return new ResponseError();
    }

    public async Task<ResponseError> CreateCustomer(CreateCustomerCommand command)
    {
        var commandAlreadyProcessed = await _customerRepository.MessageAlreadyProcessed(command);
        if (commandAlreadyProcessed)
            return new ResponseError("Mensagem já processada");

        var userAlreadyRegistered = await _customerRepository.CustomerAlreadyRegistered(command.GetDocument());
        if (userAlreadyRegistered)
            return new ResponseError("Cliente já cadastrado");

        var strategy =
            _createCustomerStrategies.FirstOrDefault(x => x.CustomerDocumentType == command.Document.DocumentType);
        if (strategy is null)
            throw new ArgumentOutOfRangeException(nameof(command.Document),
                "Strategy não encontrada para este tipo de document");

        return await strategy.CreateCustomer(command);
    }
}
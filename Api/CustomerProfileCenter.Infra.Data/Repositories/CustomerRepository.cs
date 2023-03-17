using CustomerProfileCenter.CrossCutting;
using CustomerProfileCenter.Domain.Repositories;
using CustomerProfileCenter.Domain.ValueObjects.Documents;
using MongoDB.Driver;

namespace CustomerProfileCenter.Infra.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IMongoDatabase _databaseConnection;

    public CustomerRepository(IMongoDatabase databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public Task<bool> CustomerAlreadyRegistered(IDocument document)
    {
        //TODO: Fazer logica de Hash para verificação se existe!
        throw new NotImplementedException();
    }

    public Task<bool> MessageAlreadyProcessed(IIdempotentMessage message)
    {
        //TODO: Ter uma collection separada só de controle de idempotency
        //TODO: Lembrar de marcar como processada de pois de finalizar, talvez seja melhor fazer tudo junto em uma transação.

        throw new NotImplementedException();
    }
}
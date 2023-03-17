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
}
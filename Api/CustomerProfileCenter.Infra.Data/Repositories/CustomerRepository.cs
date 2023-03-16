using CustomerProfileCenter.Domain.Repositories;
using MongoDB.Driver;

namespace CustomerProfileCenter.Infra.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IMongoDatabase _databaseConnection;

    public CustomerRepository(IMongoDatabase databaseConnection)
    {
        _databaseConnection = databaseConnection;
    }

    public async Task Example()
    {
        for (int i = 0; i < 100000; i++)
        {
            await _databaseConnection
                .GetCollection<DocumentExample>("Customer")
                .InsertOneAsync(new DocumentExample("Cristiano", DateTime.Now.ToLongTimeString()));
        }
    }
}

public record DocumentExample(string Name, string Date);
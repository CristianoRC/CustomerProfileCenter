using CustomerProfileCenter.CrossCutting;
using CustomerProfileCenter.Domain.Repositories;
using CustomerProfileCenter.Domain.ValueObjects.Documents;
using CustomerProfileCenter.Infra.Data.DatabaseObjects;
using CustomerProfileCenter.Infra.Data.HashAndCryptography;
using MongoDB.Driver;

namespace CustomerProfileCenter.Infra.Data.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly IMongoDatabase _databaseConnection;
    private readonly IDocumentSecurityService _documentSecurityService;

    public CustomerRepository(IMongoDatabase databaseConnection, IDocumentSecurityService documentSecurityService)
    {
        _databaseConnection = databaseConnection;
        _documentSecurityService = documentSecurityService;
    }

    public async Task<bool> CustomerAlreadyRegistered(IDocument document)
    {
        var documentHash = _documentSecurityService.GetDocumentHash(document);
        var customerCollection = _databaseConnection.GetCollection<Customer>("Customer");
        var customer = await customerCollection.Find(x => x.DocumentHash == documentHash).FirstOrDefaultAsync();
        return customer is not null;
    }

    public async Task<bool> MessageAlreadyProcessed(IIdempotentMessage message)
    {
        var processedMessageCollection = _databaseConnection.GetCollection<IdempotentMessage>("ProcessedMessages");
        var processedMessage = await processedMessageCollection.Find(x => x.IdempotencyKey == message.IdempotencyKey)
            .FirstOrDefaultAsync();
        return processedMessage is not null;
    }
}
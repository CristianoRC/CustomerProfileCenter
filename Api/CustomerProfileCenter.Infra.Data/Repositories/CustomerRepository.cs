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

    public Task<bool> CustomerAlreadyRegistered(IDocument document)
    {
        //TODO: Fazer logica de Hash para verificação se existe!
        throw new NotImplementedException();
    }

    public async Task<bool> MessageAlreadyProcessed(IIdempotentMessage message)
    {
        var processedMessageCollection = _databaseConnection.GetCollection<IdempotentMessage>("ProcessedMessages");

        _databaseConnection.GetCollection<IdempotentMessage>("ProcessedMessages");
        await processedMessageCollection.InsertOneAsync(new IdempotentMessage(message));

        var processedMessage = await processedMessageCollection.Find(x => x.IdempotencyKey == message.IdempotencyKey)
            .FirstOrDefaultAsync();
        return processedMessage is not null;
    }
}
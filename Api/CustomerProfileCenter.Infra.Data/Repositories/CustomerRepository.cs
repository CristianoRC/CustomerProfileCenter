using CustomerProfileCenter.Application.Customer;
using CustomerProfileCenter.CrossCutting;
using CustomerProfileCenter.Domain.Entities;
using CustomerProfileCenter.Domain.Repositories;
using CustomerProfileCenter.Domain.ValueObjects.Documents;
using CustomerProfileCenter.Infra.Data.DatabaseObjects;
using CustomerProfileCenter.Infra.Data.HashAndCryptography;
using MongoDB.Driver;
using Customer = CustomerProfileCenter.Infra.Data.DatabaseObjects.Customer;

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

    private Task MarkMessageAsProcessed(IIdempotentMessage idempotencyKey)
    {
        var processedMessageCollection = _databaseConnection.GetCollection<IdempotentMessage>("ProcessedMessages");
        return processedMessageCollection.InsertOneAsync(new IdempotentMessage(idempotencyKey));
    }

    public async Task CreateIndividual(Individual individual, IIdempotentMessage idempotencyKey)
    {
        var documentHash = _documentSecurityService.GetDocumentHash(individual.Document);
        var encryptedDocument = _documentSecurityService.EncryptDocument(individual.Document);

        var address = individual.Address is null ? null : new Address(individual.Address);
        var customer = new Customer
        {
            DocumentHash = documentHash,
            DocumentNumber = encryptedDocument,
            DocumentType = EDocumentType.Cpf,
            Name = individual.Name,
            Birthday = individual.BirthDay?.ToDateTime(TimeOnly.MinValue),
            EmailAddress = individual.Email?.Address,
            PhoneNumber = individual.PhoneNumber?.Number,
            CorporateName = string.Empty,
            Address = address
        };

        var customerCollection = _databaseConnection.GetCollection<Customer>("Customer");
        await customerCollection.InsertOneAsync(customer);
        await MarkMessageAsProcessed(idempotencyKey);
    }

    public Task CreateCompany(Company company, IIdempotentMessage idempotencyKey)
    {
        //TODO: Não salvar os campos inválidos.
        //TODO: Salvar mensagem como processada
        throw new NotImplementedException();
    }
}
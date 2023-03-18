using CustomerProfileCenter.CrossCutting;
using CustomerProfileCenter.Domain.Entities;
using CustomerProfileCenter.Domain.ValueObjects.Documents;

namespace CustomerProfileCenter.Domain.Repositories;

public interface ICustomerRepository
{
    Task<bool> CustomerAlreadyRegistered(IDocument document);

    Task<bool> MessageAlreadyProcessed(IIdempotentMessage message);
    Task CreateIndividual(Individual individual, IIdempotentMessage idempotencyKey);
    Task CreateCompany(Company company, IIdempotentMessage idempotencyKey);
}
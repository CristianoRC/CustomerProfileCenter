using CustomerProfileCenter.Domain.ValueObjects.Documents;

namespace CustomerProfileCenter.Domain.Repositories;

public interface ICustomerRepository
{
    Task<bool> CustomerAlreadyRegistered(IDocument document);
}
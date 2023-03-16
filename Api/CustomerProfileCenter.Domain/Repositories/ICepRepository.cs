using CustomerProfileCenter.Domain.Entities;
using CustomerProfileCenter.Domain.ValueObjects;

namespace CustomerProfileCenter.Domain.Repositories;

public interface ICepRepository
{
    Task<Address> GetAddress(Cep cep);
}
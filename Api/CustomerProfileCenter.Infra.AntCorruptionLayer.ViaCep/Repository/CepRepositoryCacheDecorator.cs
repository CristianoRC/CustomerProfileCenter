using CustomerProfileCenter.Domain.Entities;
using CustomerProfileCenter.Domain.Repositories;
using CustomerProfileCenter.Domain.ValueObjects;

namespace CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep.Repository;

internal class CepRepositoryCacheDecorator : ICepRepository
{
    private readonly ICepRepository _repository;

    internal CepRepositoryCacheDecorator(ICepRepository repository)
    {
        _repository = repository;
    }

    public Task<Address> GetAddress(Cep cep)
    {
        throw new NotImplementedException();
    }
}
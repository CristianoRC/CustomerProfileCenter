using CustomerProfileCenter.Domain.Entities;
using CustomerProfileCenter.Domain.Repositories;
using CustomerProfileCenter.Domain.ValueObjects;
using CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep.ViaCep;

namespace CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep.Repository;

public class CepRepositoryCacheDecorator : ICepRepository
{
    private readonly ICepRepository _repository;

    public CepRepositoryCacheDecorator(ICepRepository repository)
    {
        _repository = repository;
    }
    
    public Task<Address> GetAddress(Cep cep)
    {
        throw new NotImplementedException();
    }
}
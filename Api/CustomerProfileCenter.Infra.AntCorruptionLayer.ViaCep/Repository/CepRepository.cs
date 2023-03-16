using CustomerProfileCenter.Domain.Entities;
using CustomerProfileCenter.Domain.Repositories;
using CustomerProfileCenter.Domain.ValueObjects;
using CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep.ViaCep;

namespace CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep.Repository;

public class CepRepository : ICepRepository
{
    private readonly IViaCepClient _viaCepClient;

    internal CepRepository(IViaCepClient viaCepClient)
    {
        _viaCepClient = viaCepClient;
    }

    public async Task<Address> GetAddress(Cep cep)
    {
        var viaCepAddress = await _viaCepClient.GetCepInformation(cep.Number);
        return viaCepAddress.ToDomainAddress();
    }
}
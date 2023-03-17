using CustomerProfileCenter.Application.Response;
using CustomerProfileCenter.Domain.Repositories;
using CustomerProfileCenter.Domain.ValueObjects;

namespace CustomerProfileCenter.Application.Address;

public class AddressService : IAddressService
{
    private readonly ICepRepository _cepRepository;

    public AddressService(ICepRepository cepRepository)
    {
        _cepRepository = cepRepository;
    }

    public async Task<Response<Domain.Entities.Address>> GetAddress(string cepNumber)
    {
        var cep = new Cep(cepNumber);
        if (cep.IsValid is false)
            return new Response<Domain.Entities.Address>("CEP Inválido");

        var address = await _cepRepository.GetAddress(cep);
        return new Response<Domain.Entities.Address>(address);
    }
}
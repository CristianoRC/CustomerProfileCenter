using CustomerProfileCenter.Application.Response;

namespace CustomerProfileCenter.Application.Address;

public interface IAddressService
{
    Task<Response<Domain.ValueObjects.Address>> GetAddress(string cep);
}
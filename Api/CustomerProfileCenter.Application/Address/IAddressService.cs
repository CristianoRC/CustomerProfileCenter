using CustomerProfileCenter.Application.Response;

namespace CustomerProfileCenter.Application.Address;

public interface IAddressService
{
    Task<Response<Domain.Entities.Address>> GetAddress(string cep);
}
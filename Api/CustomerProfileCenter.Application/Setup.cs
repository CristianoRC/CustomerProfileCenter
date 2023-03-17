using CustomerProfileCenter.Application.Address;
using CustomerProfileCenter.Application.Customer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerProfileCenter.Application;

public static class Setup
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IAddressService, AddressService>();
        services.AddTransient<ICustomerService, CustomerService>();
        return services;
    }
}
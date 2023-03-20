using CustomerProfileCenter.Application.Address;
using CustomerProfileCenter.Application.Customer;
using CustomerProfileCenter.Application.Customer.Strategies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerProfileCenter.Application;

public static class Setup
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<CreateIndividualStrategy>();
        services.AddScoped<CreateCompanyStrategy>();
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<ICustomerService, CustomerService>();
        ConfigureCreateCustomerStrategyFactory(services);
        return services;
    }

    private static void ConfigureCreateCustomerStrategyFactory(IServiceCollection services)
    {
        services.AddScoped<IEnumerable<ICreateCustomerStrategy>>(provider =>
        {
            return new ICreateCustomerStrategy[]
            {
                provider.GetService<CreateIndividualStrategy>()!,
                provider.GetService<CreateCompanyStrategy>()!
            };
        });
    }
}
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
        services.AddTransient<CreateIndividualStrategy>();
        services.AddTransient<CreateCompanyStrategy>();
        services.AddTransient<IAddressService, AddressService>();
        services.AddTransient<ICustomerService, CustomerService>();
        ConfigureCreateCustomerStrategyFactory(services);
        return services;
    }

    private static void ConfigureCreateCustomerStrategyFactory(IServiceCollection services)
    {
        services.AddTransient<IEnumerable<ICreateCustomerStrategy>>(provider =>
        {
            return new ICreateCustomerStrategy[]
            {
                provider.GetService<CreateIndividualStrategy>()!,
                provider.GetService<CreateCompanyStrategy>()!
            };
        });
    }
}
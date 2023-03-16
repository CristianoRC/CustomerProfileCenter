using Microsoft.Extensions.DependencyInjection;

namespace CustomerProfileCenter.Domain;

public static class Setup
{
    public static IServiceCollection AddDomain(this IServiceCollection service)
    {
        return service;
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerProfileCenter.Infra.Data;

public static class Setup
{
    public static IServiceCollection AddData(this IServiceCollection service, IConfiguration config)
    {
        return service;
    }
}
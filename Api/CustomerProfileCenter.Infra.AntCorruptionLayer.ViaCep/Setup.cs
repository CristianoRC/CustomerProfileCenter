using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep;

public static class Setup
{
    public static IServiceCollection AddViaCep(this IServiceCollection service, IConfiguration config)
    {
        return service;
    }
}
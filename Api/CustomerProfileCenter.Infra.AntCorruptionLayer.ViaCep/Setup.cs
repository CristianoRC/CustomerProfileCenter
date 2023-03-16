using CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep.ViaCep;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep;

public static class Setup
{
    public static IServiceCollection AddViaCep(this IServiceCollection services, IConfiguration config)
    {
        services.AddRefitClient<IViaCepClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(config["ViaCep"]));
        //Adiciona Retry e Circuit Breaker

        return services;
    }
}
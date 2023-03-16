using CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep.ViaCep;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Retry;
using Refit;

namespace CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep;

public static class Setup
{
    public static IServiceCollection AddViaCep(this IServiceCollection services, IConfiguration config)
    {
        services.AddRefitClient<IViaCepClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(config["ViaCep"]));
        //.AddPolicyHandler();
        //Adiciona Retry e Circuit Breaker
        return services;
    }

    private static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 5);
        /*return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(delay);*/
        throw new NotImplementedException();
    }
}
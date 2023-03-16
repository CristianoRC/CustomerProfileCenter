using CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep.ViaCep;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using Polly.Retry;
using Refit;

namespace CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep;

public static class Setup
{
    public static IServiceCollection AddViaCep(this IServiceCollection services, IConfiguration config)
    {
        services.AddRefitClient<IViaCepClient>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(config["ViaCep"]))
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());
        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }

    private static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        var delay = Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(1), retryCount: 5);
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(delay);
    }
}
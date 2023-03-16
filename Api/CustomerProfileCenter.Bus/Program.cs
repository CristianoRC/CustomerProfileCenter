using CustomerProfileCenter.Domain;
using CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep;
using CustomerProfileCenter.Infra.Data;
using Microsoft.Extensions.Hosting;

var hostBuilder = new HostBuilder()
    .ConfigureServices((builderContext, services) =>
    {
        services
            .AddViaCep(builderContext.Configuration)
            .AddData(builderContext.Configuration)
            .AddDomain();
    })
    .ConfigureFunctionsWorkerDefaults();

var host = hostBuilder.Build();
host.Run();
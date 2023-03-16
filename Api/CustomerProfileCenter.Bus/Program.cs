using CustomerProfileCenter.Domain;
using CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep;
using CustomerProfileCenter.Infra.Data;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureServices((builderContext, services) =>
    {
        services
            .AddViaCep(builderContext.Configuration)
            .AddData(builderContext.Configuration)
            .AddDomain();
    })
    .ConfigureFunctionsWorkerDefaults()
    .Build();

host.Run();
using CustomerProfileCenter.Application;
using CustomerProfileCenter.Domain;
using CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep;
using CustomerProfileCenter.Infra.Data;
using CustomerProfileCenter.Infra.MessageBus;
using CustomerProfileCenter.Infra.MessageBus.Configuration;
using Microsoft.Extensions.Hosting;

var hostBuilder = new HostBuilder()
    .ConfigureServices((builderContext, services) =>
    {
        services
            .AddViaCep(builderContext.Configuration)
            .AddMessageBus(builderContext.Configuration)
            .AddApplication(builderContext.Configuration)
            .AddData(builderContext.Configuration, builderContext.HostingEnvironment.IsDevelopment())
            .AddDomain();
    })
    .ConfigureFunctionsWorkerDefaults();

var host = hostBuilder.Build();
host.Run();
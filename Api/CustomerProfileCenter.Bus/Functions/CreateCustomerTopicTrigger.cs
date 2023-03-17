using CustomerProfileCenter.Application.Customer;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CustomerProfileCenter.Bus.Functions;

public static class CreateCustomerTopicTrigger
{
    [Function("CreateCustomerTopicTrigger")]
    public static async Task Run([RabbitMQTrigger("CreateCustomerTopicTrigger", ConnectionStringSetting = "RabbitMQConnectionString")]
        string message, FunctionContext context)
    {
        var logger = context.GetLogger(nameof(CreateCustomerTopicTrigger));
        var customerService = context.InstanceServices.GetService<ICustomerService>();
        var createCustomerCommand = JsonConvert.DeserializeObject<CreateCustomerCommand>(message);
        var createCustomerResponse = await customerService.CreateCustomer(createCustomerCommand);
        if (createCustomerResponse.HasError)
            logger.LogError("Erro ao criar um novo consumidor. Erro: {Error}, Command: {Command}",
                createCustomerResponse.ErrorMessage, createCustomerCommand);
        //Se o sistema de log não esconde esses dados sensíveis como documento, seria necessário implementar isso aqui na hora de fazer log
    }
}
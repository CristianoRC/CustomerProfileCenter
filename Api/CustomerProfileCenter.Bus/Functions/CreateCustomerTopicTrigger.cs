using CustomerProfileCenter.Application.Customer;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CustomerProfileCenter.Bus.Functions;

public static class CreateCustomerTopicTrigger
{
    [Function("CreateCustomerTopicTrigger")]
    public static void Run(
        [RabbitMQTrigger("CreateCustomerTopicTrigger", ConnectionStringSetting = "RabbitMQConnectionString")]
        string message,
        ILogger log, FunctionContext context)
    {
        var createCustomerCommand = JsonConvert.DeserializeObject<CreateCustomerCommand>(message);

        var logger = context.GetLogger("CreateCustomerTopicTrigger");
        logger.LogInformation($"C# Queue trigger function processed: {createCustomerCommand}");
    }
}
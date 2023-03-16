using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CustomerProfileCenter.Bus.Functions;

public static class CreateCustomerTopicTrigger
{
    [Function("CreateCustomerTopicTrigger")]
    public static void Run([RabbitMQTrigger("example", ConnectionStringSetting = "RabbitMQConnectionString")] string message,
        ILogger log, FunctionContext context)
    {
        var logger = context.GetLogger("CreateCustomerTopicTrigger");
        logger.LogInformation($"C# Queue trigger function processed: {message}");
    }
}
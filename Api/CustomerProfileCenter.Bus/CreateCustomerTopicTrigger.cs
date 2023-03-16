using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace CustomerProfileCenter.Bus;

public static class CreateCustomerTopicTrigger
{
    [Function("CreateCustomerTopicTrigger")]
    public static void Run([RabbitMQTrigger("queue")] string message, ILogger log, FunctionContext context)
    {
        //TODO: Colocar connection string na chave rabbitMQConnection
        var logger = context.GetLogger("CreateCustomerTopicTrigger");
        logger.LogInformation($"C# Queue trigger function processed: {message}");
    }
}
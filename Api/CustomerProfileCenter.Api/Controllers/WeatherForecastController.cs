using CustomerProfileCenter.Infra.MessageBus;
using Microsoft.AspNetCore.Mvc;

namespace CustomerProfileCenter.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ISendMessageService _sendMessageService;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ISendMessageService sendMessageService, ILogger<WeatherForecastController> logger)
    {
        _sendMessageService = sendMessageService;
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get()
    {
        _sendMessageService.SendMessage(new {Name = "Example RabbitMQ - Sender"}, "example");
        return Ok();
    }
}
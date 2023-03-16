using CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep.ViaCep;
using Microsoft.AspNetCore.Mvc;

namespace CustomerProfileCenter.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Infomração {Message}", new {Message = "123"});
        _logger.LogError("Erro!");
        _logger.LogCritical("Crítico!");
        return Ok();
    }
}
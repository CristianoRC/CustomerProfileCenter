using CustomerProfileCenter.Domain.Repositories;
using CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep.ViaCep;
using Microsoft.AspNetCore.Mvc;

namespace CustomerProfileCenter.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ICustomerRepository _repository;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ICustomerRepository repository, ILogger<WeatherForecastController> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Infomração {Message}", new {Message = "123"});
        _logger.LogError("Erro!");
        _logger.LogCritical("Crítico!");
        await _repository.Example();
        return Ok();
    }
}
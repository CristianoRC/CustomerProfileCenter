using CustomerProfileCenter.Domain.Repositories;
using CustomerProfileCenter.Domain.ValueObjects;
using CustomerProfileCenter.Infra.AntCorruptionLayer.ViaCep.ViaCep;
using Microsoft.AspNetCore.Mvc;

namespace CustomerProfileCenter.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ICepRepository _cepRepository;
    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ICepRepository cepRepository, ILogger<WeatherForecastController> logger)
    {
        _cepRepository = cepRepository;
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get()
    {
        _logger.LogInformation("Infomração {Message}", new {Message = "123"});
        _logger.LogError("Erro!");
        _logger.LogCritical("Crítico!");
        await _cepRepository.GetAddress(new Cep("96085000"));
        return Ok();
    }
}
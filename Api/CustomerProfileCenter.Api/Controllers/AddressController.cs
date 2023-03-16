using Microsoft.AspNetCore.Mvc;

namespace CustomerProfileCenter.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : Controller
{
    [HttpGet("/{cep}")]
    public async Task<IActionResult> GetAddress(string cep)
    {
        return Ok();
    }
}
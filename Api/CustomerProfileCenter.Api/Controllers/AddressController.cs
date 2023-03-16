using CustomerProfileCenter.Application.Address;
using Microsoft.AspNetCore.Mvc;

namespace CustomerProfileCenter.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : Controller
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet("{cep}")]
    public async Task<IActionResult> GetAddress(string cep)
    {
        var address = await _addressService.GetAddress(cep);
        if (address.Error.HasError)
            return BadRequest(address);
        return Ok(address);
    }
}
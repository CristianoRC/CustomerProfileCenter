using CustomerProfileCenter.Application.Address;
using CustomerProfileCenter.Application.Response;
using CustomerProfileCenter.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(Response<Address>))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, Type = typeof(Response<Address>))]
    public async Task<IActionResult> GetAddress(string cep)
    {
        var address = await _addressService.GetAddress(cep);
        if (address.Error.HasError)
            return BadRequest(address);
        return Ok(address);
    }
}
using CustomerProfileCenter.Application.Customer;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CustomerProfileCenter.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : Controller
{
    [HttpPost]
    [SwaggerResponse(StatusCodes.Status202Accepted)]
    [SwaggerResponse(StatusCodes.Status409Conflict)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public IActionResult CreateCustomer([FromBody] CreateCustomerCommand createCustomerCommand)
    {
        //TODO: Validar mensagem de erro "Cliente já cadastrado"  para o 409, o Resto é 400
        return Accepted();
    }
}
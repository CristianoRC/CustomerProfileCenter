using CustomerProfileCenter.Application.Customer;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace CustomerProfileCenter.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    [SwaggerResponse(StatusCodes.Status202Accepted)]
    [SwaggerResponse(StatusCodes.Status409Conflict)]
    [SwaggerResponse(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand createCustomerCommand)
    {
        var createCustomerResponse = await _customerService.EnqueueCreateCustomerCommand(createCustomerCommand);
        if (createCustomerResponse.HasError is false)
            return new AcceptedResult();

        if (ShouldReturnConflictResponse(createCustomerResponse.ErrorMessage))
            return Conflict();

        return BadRequest(createCustomerResponse);
    }

    private static bool ShouldReturnConflictResponse(string errorMessage)
    {
        return errorMessage.Equals("Cliente já cadastrado", StringComparison.InvariantCultureIgnoreCase);
    }
}
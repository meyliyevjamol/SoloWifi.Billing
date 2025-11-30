using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoloWifi.Billing.ServiceLayer;
using SoloWifi.Billing.ServiceLayer.Services;

namespace SoloWifi.Billing.WebApi;

[ApiController]
[Route("[controller]/[action]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomerController(ICustomerService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _service.GetAllAsync());
    }
}

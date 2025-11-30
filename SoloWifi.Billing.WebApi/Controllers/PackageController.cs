using Microsoft.AspNetCore.Mvc;
using SoloWifi.Billing.ServiceLayer;

namespace SoloWifi.Billing.WebApi;

[ApiController]
[Route("[controller]/[action]")]
public class PackageController : ControllerBase
{
    private readonly IPackageService _service;

    public PackageController(IPackageService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _service.GetAllAsync());
    }
}


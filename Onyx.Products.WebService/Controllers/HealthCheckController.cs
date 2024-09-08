using Microsoft.AspNetCore.Mvc;

namespace Onyx.Products.WebService.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}

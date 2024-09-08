using Microsoft.AspNetCore.Mvc;
using Onyx.Products.WebService.Core;

namespace Onyx.Products.WebService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ColourController : ControllerBase
{
    private readonly IColourStore _colourStore;

    public ColourController(IColourStore colourStore)
    {
        _colourStore = colourStore;
    }

    [HttpGet]
    public async Task<ActionResult<IDictionary<int, string>>> GetAllAsync()
    {
        var colours = await _colourStore.GetAllAsync();
        return Ok(colours);
    }
}

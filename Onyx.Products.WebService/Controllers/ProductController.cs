using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Onyx.Products.WebService.Core;

namespace Onyx.Products.WebService.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductStore _productStore;

    public ProductController(IProductStore productStore)
    {
        _productStore = productStore;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync()
    {
        var products = await _productStore.GetAllAsync();
        return Ok(products);
    }

    [HttpGet("colourId/{colourId}")]
    public async Task<ActionResult<IEnumerable<Product>>> GetByColourIdAsync(int colourId)
    {
        var products = await _productStore.GetByColourIdAsync(colourId);
        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateAsync([FromBody] NewProductRequest newProductRequest)
    {
        newProductRequest.CreatedBy = HttpContext.User.Identity?.Name ?? "Anon";

        var result = await _productStore.CreateAsync(newProductRequest);

        if(result.IsSuccess)
        {
            return Ok(result.Product);
        }

        return BadRequest(result.Errors);
    }
}

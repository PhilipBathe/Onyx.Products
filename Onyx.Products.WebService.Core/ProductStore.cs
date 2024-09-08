using Microsoft.Extensions.Logging;
using Onyx.Products.Messages.Events;
using Onyx.Products.WebService.Core.Database;
using Onyx.Products.WebService.Core.Events;

namespace Onyx.Products.WebService.Core;

public class ProductStore : IProductStore
{
    private readonly ILogger<ProductStore> _logger;
    private readonly IProductData _productData;
    private readonly IProductEvents _productEvents;
    private readonly IProductValidator _productValidator;

    public ProductStore(
        ILogger<ProductStore> logger, 
        IProductData productData, 
        IProductEvents productEvents, 
        IProductValidator productValidator)
    {
        _logger = logger;
        _productData = productData;
        _productEvents = productEvents;
        _productValidator = productValidator;
    }

    public async Task<NewProductResult> CreateAsync(NewProductRequest newProductRequest)
    {
        _logger.LogInformation("New product request placed by {CreatedBy}", newProductRequest.CreatedBy);

        var result = new NewProductResult();

        result.Errors = await _productValidator.ValidateAsync(newProductRequest);

        if(result.IsSuccess == false)
        {
            _logger.LogInformation("New product request failed due to {@Errors}", result.Errors);
            return result;
        }

        result.Product = await _productData.CreateAsync(newProductRequest);

        _logger.LogInformation("New product created successfully with ID {guid}", result.Product.Guid);

        await _productEvents.PublishCreatedAsync(new ProductCreated { Guid = result.Product.Guid });

        return result;
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return _productData.GetAllAsync();
    }

    public Task<IEnumerable<Product>> GetByColourIdAsync(int colourId)
    {
        return _productData.GetByColourIdAsync(colourId);
    }
}

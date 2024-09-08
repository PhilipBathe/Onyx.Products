using Microsoft.Extensions.Logging;
using Onyx.Products.Messages.Events;
using Onyx.Products.WebService.Core.Events;

namespace Onyx.Products.WebService.Events;

public class ProductEvents : IProductEvents
{
    private readonly ILogger<ProductEvents> _logger;

    public ProductEvents(ILogger<ProductEvents> logger)
    {
        _logger = logger;
    }

    public Task PublishCreatedAsync(ProductCreated productCreated)
    {
        _logger.LogInformation("PublishCreated event with Guid {Guid} was published", productCreated.Guid);

        //This would publish to a Topic in a fully working system
        return Task.CompletedTask;
    }
}

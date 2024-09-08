using Onyx.Products.Messages.Events;

namespace Onyx.Products.WebService.Core.Events;

public interface IProductEvents
{
    Task PublishCreatedAsync(ProductCreated productCreated);
}

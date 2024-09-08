namespace Onyx.Products.WebService.Core;

public interface IProductValidator
{
    Task<IList<string>> ValidateAsync(NewProductRequest newProductRequest);
}

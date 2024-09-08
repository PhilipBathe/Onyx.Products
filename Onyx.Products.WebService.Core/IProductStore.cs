namespace Onyx.Products.WebService.Core;

public interface IProductStore
{
    Task<NewProductResult> CreateAsync(NewProductRequest newProductRequest);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetByColourIdAsync(int colourId);
}

namespace Onyx.Products.WebService.Core.Database;

public interface IProductData
{
    Task<Product> CreateAsync(NewProductRequest newProductRequest);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetByColourIdAsync(int colourId);
    Task<Product?> GetByIdAsync(Guid guid);
}

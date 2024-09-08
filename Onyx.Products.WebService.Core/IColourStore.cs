namespace Onyx.Products.WebService.Core;

public interface IColourStore
{
    Task<IDictionary<int, string>> GetAllAsync();
}

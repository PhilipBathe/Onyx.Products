namespace Onyx.Products.WebService.Core.Database;

public interface IColourData
{
    Task<IDictionary<int, string>> GetAllAsync();
    Task<string?> GetByIdAsync(int id);
}

using Onyx.Products.WebService.Core.Database;

namespace Onyx.Products.WebService.Core;

public class ColourStore : IColourStore
{
    private readonly IColourData _colourData;

    public ColourStore(IColourData colourData)
    {
        _colourData = colourData;
    }

    public Task<IDictionary<int, string>> GetAllAsync()
    {
        return _colourData.GetAllAsync();
    }
}

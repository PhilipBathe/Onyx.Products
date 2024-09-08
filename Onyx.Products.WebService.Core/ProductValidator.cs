using Onyx.Products.WebService.Core.Database;

namespace Onyx.Products.WebService.Core;

public class ProductValidator : IProductValidator
{
    private readonly IColourData _colourData;

    public ProductValidator(IColourData colourData)
    {
        _colourData = colourData;
    }

    public async Task<IList<string>> ValidateAsync(NewProductRequest newProductRequest)
    {
        var errors = new List<string>();

        if (string.IsNullOrEmpty(newProductRequest.Name) || newProductRequest.Name?.Length <= 3)
        {
            errors.Add(ProductValidationMessages.NameMinLength);
        }

        if(newProductRequest.Name?.Length > 50)
        {
            errors.Add(ProductValidationMessages.NameMaxLength);
        }

        if(newProductRequest.Description?.Length > 500)
        {
            errors.Add(ProductValidationMessages.DescriptionMaxLength);
        }

        if(newProductRequest.Price <= 0)
        {
            errors.Add(ProductValidationMessages.PriceRequired);
        }

        if (newProductRequest.Price > 1_000_000_000)
        {
            errors.Add(ProductValidationMessages.PriceMaximum);
        }

        if(string.IsNullOrEmpty(await _colourData.GetByIdAsync(newProductRequest.ColourId)))
        {
            errors.Add(ProductValidationMessages.ColourNotFound);
        }

        return errors;
    }
}

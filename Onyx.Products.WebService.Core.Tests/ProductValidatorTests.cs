using Onyx.Products.WebService.Core.Database;

namespace Onyx.Products.WebService.Core.Tests;

public class ProductValidatorTests
{
    protected Mock<IColourData> _mockColourData;

    protected ProductValidator _productValidator;

    protected NewProductRequest _newProductRequest;

    [SetUp]
    public void SetUp()
    {
        _mockColourData = new Mock<IColourData>();

        _mockColourData.Setup(m => m.GetByIdAsync(It.IsAny<int>())).ReturnsAsync("foo");

        _productValidator = new ProductValidator(_mockColourData.Object);

        _newProductRequest = new NewProductRequest { Name = "some name", Price = 1 };
    }

    [Test]
    public async Task ShouldPassValidRequest()
    {
        var result = await _productValidator.ValidateAsync(_newProductRequest);

        result.ShouldBeEmpty();
    }

    [Test]
    public async Task ShouldRequireMinimumValues()
    {
        _newProductRequest.Name = "foo";
        _newProductRequest.Price = 0;

        var result = await _productValidator.ValidateAsync(_newProductRequest);

        result.ShouldContain(ProductValidationMessages.NameMinLength);
        result.ShouldContain(ProductValidationMessages.PriceRequired);
    }

    [Test]
    public async Task ShouldPreventMaximumValuesBeingExceeded()
    {
        _newProductRequest.Name = "This name is longer than fifty characters 123456789012345678901234567890";
        _newProductRequest.Description = 
            @"This description is longer than five hundred characters 123456789012345678901234567890
            This description is longer than five hundred characters 123456789012345678901234567890
            This description is longer than five hundred characters 123456789012345678901234567890
            This description is longer than five hundred characters 123456789012345678901234567890
            This description is longer than five hundred characters 123456789012345678901234567890
            This description is longer than five hundred characters 123456789012345678901234567890
            This description is longer than five hundred characters 123456789012345678901234567890
            This description is longer than five hundred characters 123456789012345678901234567890
            This description is longer than five hundred characters 123456789012345678901234567890
            This description is longer than five hundred characters 123456789012345678901234567890
            This description is longer than five hundred characters 123456789012345678901234567890
            ";
        _newProductRequest.Price = 2_000_000_000.99m;

        var result = await _productValidator.ValidateAsync(_newProductRequest);

        result.ShouldContain(ProductValidationMessages.NameMaxLength);
        result.ShouldContain(ProductValidationMessages.DescriptionMaxLength);
        result.ShouldContain(ProductValidationMessages.PriceMaximum);
    }

    [Test]
    public async Task ShouldCheckColourExists()
    {
        _mockColourData.Setup(m => m.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((string)null);

        var result = await _productValidator.ValidateAsync(_newProductRequest);

        result.ShouldContain(ProductValidationMessages.ColourNotFound);
    }
}

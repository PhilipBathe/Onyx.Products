using Onyx.Products.Messages.Events;
using Onyx.Products.WebService.Core.Database;
using Onyx.Products.WebService.Core.Events;

namespace Onyx.Products.WebService.Core.Tests;

public class ProductStoreTests
{
    protected Mock<ILogger<ProductStore>> _mockLogger;
    protected Mock<IProductData> _mockProductData;
    protected Mock<IProductEvents> _mockProductEvents;
    protected Mock<IProductValidator> _mockProductValidator;

    protected ProductStore _productStore;

    protected NewProductRequest _newProductRequest;

    [SetUp]
    public void Setup()
    {
        _mockLogger = new Mock<ILogger<ProductStore>>();
        _mockProductData = new Mock<IProductData>();
        _mockProductEvents = new Mock<IProductEvents>();
        _mockProductValidator = new Mock<IProductValidator>();

        _productStore = new ProductStore(
            _mockLogger.Object,
            _mockProductData.Object,
            _mockProductEvents.Object,
            _mockProductValidator.Object
            );

        _newProductRequest = new NewProductRequest { };
    }

    public class CreateAsyncMethod : ProductStoreTests
    {
        private Guid guid;
        private Product product;

        [SetUp]
        public void MethodSetUp()
        {
            guid = Guid.NewGuid();
            product = new Product { Guid = guid };

            _mockProductData.Setup(m => m.CreateAsync(It.IsAny<NewProductRequest>())).ReturnsAsync(product);
            _mockProductValidator.Setup(m => m.ValidateAsync(It.IsAny<NewProductRequest>())).ReturnsAsync(new List<string>());
        }

        [Test]
        public async Task ShouldReturnCreatedProduct()
        {
            var result = await _productStore.CreateAsync(_newProductRequest);

            result.Product.Guid.ShouldBe(guid);
        }

        [Test]
        public async Task ShouldPublishProductCreatedEvent()
        {
            var result = await _productStore.CreateAsync(_newProductRequest);

            _mockProductEvents.Verify(m => m.PublishCreatedAsync(It.Is<ProductCreated>(pc => pc.Guid == guid)), Times.Once);
        }

        [Test]
        public async Task ShouldReturnIfFailedValidation()
        {
            var errors = new List<string> { "foo" };
            _mockProductValidator.Setup(m => m.ValidateAsync(It.IsAny<NewProductRequest>())).ReturnsAsync(errors);

            var result = await _productStore.CreateAsync(_newProductRequest);

            result.IsSuccess.ShouldBeFalse();
            result.Errors.ShouldBe(errors);

            _mockLogger.Verify(l =>
                l.Log(LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => o.ToString().StartsWith("New product request failed due to ") && t.Name == "FormattedLogValues"),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)
                ), Times.Once);

            _mockProductData.Verify(m => m.CreateAsync(It.IsAny<NewProductRequest>()), Times.Never);
            _mockProductEvents.Verify(m => m.PublishCreatedAsync(It.IsAny<ProductCreated>()), Times.Never);
        }
    }
}
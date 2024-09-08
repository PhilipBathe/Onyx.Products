namespace Onyx.Products.WebService.Core;

public class NewProductResult
{
    public IList<string> Errors = new List<string>();
    public Product Product { get; set; }
    public bool IsSuccess => Errors.Any() == false;
}

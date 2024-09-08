namespace Onyx.Products.WebService.Core;

public static class ProductValidationMessages
{
    public static string NameMinLength = "Name must be at least 3 characters";
    public static string NameMaxLength = "Name must be no more than 50 characters";

    public static string DescriptionMaxLength = "Description must be no more than 500 characters";

    public static string PriceRequired = "Price must be greater than 0";
    public static string PriceMaximum = "Price must be less than 1,000,000,000";

    public static string ColourNotFound = "Invalid colour selected";
}

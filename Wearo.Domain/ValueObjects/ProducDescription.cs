using Wearo.Domain.Abstractions;

namespace Wearo.Domain.ValueObjects;

public class ProductDescription : ValueObject
{
    public ProductDescription(string brand,
        string title,
        string? description = null,
        string? articleNumber = null)
    {
        if (string.IsNullOrEmpty(brand))
            throw new ArgumentException("Model lenght cannot be 0");

        if (string.IsNullOrEmpty(title))
            throw new ArgumentException("Name length cannot be 0");

        Brand = brand;
        Title = title;
        Description = description;
        ArticleNumber = articleNumber;
    }

    public string Brand { get; private set; }

    public string Title { get; private set; }

    public string? Description { get; private set; }

    public string? ArticleNumber { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Brand;
        yield return Title;
        yield return Description;
        yield return ArticleNumber;
    }
}


using Wearo.Domain.Abstractions;
using Wearo.Domain.ValueObjects;

namespace Wearo.Domain.Entities;

public class Product : Entity<Guid>
{
    private readonly List<SizeOption> _availableSizes = new();
    public IReadOnlyCollection<SizeOption> AvailableSizes => _availableSizes.AsReadOnly();

    private Product() : base() { }

    private Product(
        Guid id,
        decimal price,
        Guid categoryId,
        ProductDescription description,
        IEnumerable<SizeOption>? sizes = null
    ) : base(id)
    {
        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero", nameof(price));

        Description = description ?? throw new ArgumentNullException(nameof(description));
        CategoryId = categoryId;
        Price = price;

        if (sizes is not null)
            UpdateSizes(sizes);
    }

    public decimal Price { get; private set; }

    public Category Category { get; private set; }

    public Guid CategoryId { get; private set; }

    public ProductDescription Description { get; private set; }

    public static Product Create(
        Guid id,
        decimal price,
        Guid categoryId,
        ProductDescription description,
        IEnumerable<SizeOption>? sizes = null)
    {
        return new Product(
            id,
            price,
            categoryId,
            description,
            sizes);
    }

    public void AddSize(SizeOption size)
    {
        if (size is null)
            throw new ArgumentNullException(nameof(size));

        if (_availableSizes.Contains(size))
            throw new InvalidOperationException("Size already exists for this product");

        _availableSizes.Add(size);
    }

    public void Update(
        decimal price,
        Guid categoryId,
        ProductDescription description,
        IEnumerable<SizeOption> sizes)
    {
        if (price <= 0)
            throw new ArgumentException("Price must be greater than zero", nameof(price));

        Description = description ?? throw new ArgumentNullException(nameof(description));
        CategoryId = categoryId;
        Price = price;

        UpdateSizes(sizes);
    }

    private void UpdateSizes(IEnumerable<SizeOption> newSizes)
    {
        if (newSizes is null)
            throw new ArgumentNullException(nameof(newSizes));

        _availableSizes.Clear();

        foreach (var size in newSizes.Distinct())
        {
            _availableSizes.Add(size);
        }
    }
}

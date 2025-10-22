using Wearo.Domain.Abstractions;

namespace Wearo.Domain.Entities;

public class Category : Entity<Guid>
{
    private Category() : base () { }

    private Category(Guid id, string categoryAttributes) : base(id)
    {
        CategoryAttributes = categoryAttributes;
    }

    public string CategoryAttributes { get; set; }

    public static Category Create(Guid id, string name)
    {
        if(string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException("Name can not be null or empty", nameof(name));
        
        return new Category(id, name);
    }
}

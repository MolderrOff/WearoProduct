using Wearo.Domain.Abstractions;

namespace Wearo.Domain.ValueObjects;

public class SizeOption : ValueObject
{
    public string Value { get; private set; }

    public SizeOption(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Size cannot be empty");

        Value = value.ToUpperInvariant();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}


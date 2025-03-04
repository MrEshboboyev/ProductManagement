using ProductManagement.Domain.Exceptions;

namespace ProductManagement.Domain.ValueObjects;

public class Sku : ValueObject
{
    public string Value { get; private set; }

    private Sku(string value)
    {
        Value = value;
    }

    public static Sku Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("SKU cannot be empty");

        // Additional validation rules for SKU format could be added here
        if (!value.All(c => char.IsLetterOrDigit(c) || c == '-'))
            throw new DomainException("SKU can only contain alphanumeric characters and hyphens");

        return new Sku(value.ToUpper());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
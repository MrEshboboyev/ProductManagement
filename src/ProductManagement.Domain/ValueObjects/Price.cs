using ProductManagement.Domain.Exceptions;

namespace ProductManagement.Domain.ValueObjects;

public class Price : ValueObject
{
    public decimal Amount { get; private set; }
    public string Currency { get; private set; }

    private Price(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Price Create(decimal amount, string currency = "USD")
    {
        if (amount < 0)
            throw new DomainException("Price cannot be negative");

        if (string.IsNullOrWhiteSpace(currency))
            throw new DomainException("Currency cannot be empty");

        return new Price(amount, currency);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Amount;
        yield return Currency;
    }

    public override string ToString() => $"{Amount} {Currency}";
}
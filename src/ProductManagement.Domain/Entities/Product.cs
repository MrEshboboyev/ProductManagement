using ProductManagement.Domain.Events;
using ProductManagement.Domain.Exceptions;
using ProductManagement.Domain.ValueObjects;

namespace ProductManagement.Domain.Entities;

public class Product : AggregateRoot
{
    public ProductId Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Price Price { get; private set; }
    public Sku Sku { get; private set; }
    public int StockQuantity { get; private set; }
    public bool IsAvailable { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Product() { }

    private Product(ProductId id, string name, string description, Price price, Sku sku, int stockQuantity)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Sku = sku;
        StockQuantity = stockQuantity;
        IsAvailable = stockQuantity > 0;
        CreatedAt = DateTime.UtcNow;

        AddDomainEvent(new ProductCreatedEvent(this));
    }

    public static Product Create(ProductId id, string name, string description, Price price, Sku sku, int stockQuantity)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name cannot be empty");

        if (stockQuantity < 0)
            throw new DomainException("Stock quantity cannot be negative");

        return new Product(id, name, description, price, sku, stockQuantity);
    }

    public void UpdateDetails(string name, string description, Price price)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Product name cannot be empty");

        Name = name;
        Description = description;
        Price = price;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateStock(int quantity)
    {
        if (quantity < 0)
            throw new DomainException("Stock quantity cannot be negative");

        StockQuantity = quantity;
        IsAvailable = quantity > 0;
        UpdatedAt = DateTime.UtcNow;
    }
}
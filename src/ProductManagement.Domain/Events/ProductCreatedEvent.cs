using ProductManagement.Domain.Entities;

namespace ProductManagement.Domain.Events;

public class ProductCreatedEvent(Product product) : IDomainEvent
{
    public Product Product { get; } = product;
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}


using MediatR;

namespace ProductManagement.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest<bool>
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; } = "USD";
    public int? StockQuantity { get; set; }
}
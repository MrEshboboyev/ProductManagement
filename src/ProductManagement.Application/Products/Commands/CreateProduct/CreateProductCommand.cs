using MediatR;

namespace ProductManagement.Application.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<string>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; } = "USD";
    public string Sku { get; set; }
    public int StockQuantity { get; set; }
}
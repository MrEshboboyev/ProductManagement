using MediatR;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Repositories;
using ProductManagement.Domain.ValueObjects;

namespace ProductManagement.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, string>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<string> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        // Create value objects
        var sku = Sku.Create(request.Sku);

        // Check if product with this SKU already exists
        if (await _productRepository.ExistsBySkuAsync(sku, cancellationToken))
            throw new ApplicationException($"Product with SKU '{request.Sku}' already exists");

        var productId = ProductId.CreateUnique();
        var price = Price.Create(request.Price, request.Currency);

        // Create domain entity
        var product = Product.Create(
            productId,
            request.Name,
            request.Description,
            price,
            sku,
            request.StockQuantity
        );

        // Save to repository
        await _productRepository.AddAsync(product, cancellationToken);

        // Return the ID of the new product
        return productId.Value;
    }
}
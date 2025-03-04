using MediatR;
using ProductManagement.Domain.Repositories;
using ProductManagement.Domain.ValueObjects;

namespace ProductManagement.Application.Products.Commands.UpdateProduct;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, bool>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var productId = ProductId.Create(request.Id);
        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);

        if (product == null)
            throw new ApplicationException($"Product with ID '{request.Id}' not found");

        // Update product details
        var price = Price.Create(request.Price, request.Currency);
        product.UpdateDetails(request.Name, request.Description, price);

        // Update stock if provided
        if (request.StockQuantity.HasValue)
        {
            product.UpdateStock(request.StockQuantity.Value);
        }

        // Save changes
        await _productRepository.UpdateAsync(product, cancellationToken);

        return true;
    }
}
using AutoMapper;
using MediatR;
using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Repositories;
using ProductManagement.Domain.ValueObjects;

namespace ProductManagement.Application.Products.Queries.GetProduct;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ProductDto> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var productId = ProductId.Create(request.Id);
        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);

        if (product == null)
            throw new ApplicationException($"Product with ID '{request.Id}' not found");

        return _mapper.Map<ProductDto>(product);
    }
}
using MediatR;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Products.Queries.GetProduct;

public class GetProductQuery : IRequest<ProductDto>
{
    public string Id { get; set; }
}

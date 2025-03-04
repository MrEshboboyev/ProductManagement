using MediatR;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Products.Queries.GetAllProducts;

public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
{
    // Could add filtering/pagination parameters here
}
using MediatR;
using FluentResults;
using vsa_journey.Domain.Entities.Product;
using vsa_journey.Application.Common.PaginatedResult;

namespace vsa_journey.Features.Products.GetAllProducts.Query;

public sealed record GetAllProductsQuery : IRequest<Result<PaginatedResult<Product>>>
{
    public int Take { get; set; } = 10;
    
    public int Skip { get; set; } = 0;
};
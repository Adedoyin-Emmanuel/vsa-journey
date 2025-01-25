using FluentResults;
using MediatR;
using vsa_journey.Domain.Entities.Product;

namespace vsa_journey.Features.Products.GetProductById.Query;

public sealed record GetProductByIdQuery : IRequest<Result<GetProductByIdResponse>>
{
    public Guid Id { get; set; }
}
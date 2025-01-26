using FluentResults;
using MediatR;

namespace vsa_journey.Features.Products.DeleteProduct.Command;

public sealed record  DeleteProductCommand : IRequest<Result<object>>
{
    public Guid Id { get; init; }
}
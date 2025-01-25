using MediatR;
using FluentResults;

namespace vsa_journey.Features.Products.UpdateProduct.Command;

public sealed record UpdateProductCommand : IRequest<Result<UpdateProductResponse>>
{
    public Guid Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public decimal? Price { get; init; }
    public bool? IsPublished { get; init; }
    public List<string>? Tags { get; init; }
    public int? Quantity { get; init; }
    public IFormFileCollection? Files { get; init; }
}
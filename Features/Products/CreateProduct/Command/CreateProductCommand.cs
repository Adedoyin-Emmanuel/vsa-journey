using MediatR;
using FluentResults;

namespace vsa_journey.Features.Products.CreateProduct.Command;

public sealed record CreateProductCommand : IRequest<Result<CreateProductResponse>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    
    public bool ?IsPublished { get; set; } = true;
    public List<string> Tags { get; set; }
    public int Quantity { get; set; }
    public Guid CategoryId { get; set; }
    public IFormFileCollection Files { get; set; }
}
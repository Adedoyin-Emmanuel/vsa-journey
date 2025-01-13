using FluentResults;
using MediatR;
using vsa_journey.Features.Products.CreateProduct.Command;

namespace vsa_journey.Features.Products.CreateProduct;

public sealed record CreateProductCommand : IRequest<Result<CreateProductResponse>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    
    public bool ?IsPublished { get; set; } = true;
    public ICollection<string> Tags { get; set; }
    public int Quantity { get; set; }
    public Guid CategoryId { get; set; }
    
    public IFormFileCollection Files { get; set; }
}
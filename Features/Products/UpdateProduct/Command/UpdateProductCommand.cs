namespace vsa_journey.Features.Products.UpdateProduct.Command;

public sealed record UpdateProductCommand
{
    public string Name { get; init; }
    public string Description { get; init; }
    public decimal Price { get; init; }
    public bool ?IsPublished { get; init; }
    public List<string> Tags { get; init; }
    public int Quantity { get; init; }
    public IFormFileCollection Files { get; init; }
}
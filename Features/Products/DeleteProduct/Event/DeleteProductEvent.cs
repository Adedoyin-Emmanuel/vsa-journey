namespace vsa_journey.Features.Products.DeleteProduct.Event;

public sealed record DeleteProductEvent
{
    public List<string> FilePaths { get; init; }
}
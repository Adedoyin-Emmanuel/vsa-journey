using MediatR;

namespace vsa_journey.Features.Products.DeleteProduct.Event;

public sealed record DeleteProductEvent : INotification
{
    public List<string> FilePaths { get; init; }
}
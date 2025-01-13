using vsa_journey.Domain.Entities.Product;

namespace vsa_journey.Features.Products.CreateProduct.Command;

public record CreateProductResponse
{
   public Guid Id { get; init; }
   public string Name { get; init; }
   public string Description { get;  init; }
   public decimal Price { get;  init; }
   public bool IsPublished { get;  init; }
   public string Status { get;  init; }
   public string BaseImageUrl { get;  init; }
   public ICollection<string> Images { get;  init; }
   public ICollection<string> Tags { get; init; }
   public Guid CategoryId { get;  init; }
   public int Quantity { get;  init; }
   public DateTime CreatedAt { get;  init; }
   public DateTime UpdatedAt { get;  init; }
}

namespace vsa_journey.Features.Products.GetProductById.Query;

public record GetProductByIdResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public bool IsPublished { get; set; }
    public string Status { get; set; }
    public string BaseImageUrl { get; set; }
    public List<string> Images { get; set; }
    public List<string> Tags { get; set; }
    public Guid CategoryId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; } 
    
}